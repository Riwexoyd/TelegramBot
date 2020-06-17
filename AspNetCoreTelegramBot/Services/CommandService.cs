using AspNetCoreTelegramBot.Attributes;
using AspNetCoreTelegramBot.Commands;
using AspNetCoreTelegramBot.Commands.Extensions;
using AspNetCoreTelegramBot.Enums.Extensions;
using AspNetCoreTelegramBot.Helpers;
using AspNetCoreTelegramBot.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Telegram.Bot;

namespace AspNetCoreTelegramBot.Services
{
    /// <summary>
    /// Сервис команд Telegram бота
    /// </summary>
    public class CommandService : ICommandService
    {
        private readonly IEnumerable<IBotCommand> commands;
        private readonly ITelegramBotClient telegramBotClient;

        private Regex commandRegex;

        public CommandService(ITelegramBotClient telegramBotClient, IEnumerable<IBotCommand> commands)
        {
            this.telegramBotClient = telegramBotClient;
            this.commands = commands;
        }

        /// <summary>
        /// Инициализировать асинхонно
        /// </summary>
        /// <returns></returns>
        public async Task InitializeAsync()
        {
            if (commandRegex != null)
            {
                return;
            }

            var bot = await telegramBotClient.GetMeAsync();
            commandRegex = new Regex($@"^\/[A-Za-z]+(@{bot.Username})?$", RegexOptions.IgnoreCase);
        }

        private bool ContainsCommand(string command)
        {
            ExceptionHelper.ThrowIfNull(commandRegex, "commandRegex");
            return commands.Any(i => i.GetCommandName() == ParseCommand(command));
        }

        private IBotCommand GetCommand(string command)
        {
            if (!ContainsCommand(command))
            {
                throw new ArgumentException("Invalid command");
            }

            return commands.FirstOrDefault(i => i.GetCommandName() == ParseCommand(command));
        }

        private string ParseCommand(string command)
        {
            return command.ToLower().Split('@').First().Substring(1);
        }

        /// <summary>
        /// Проверка строки на команду
        /// </summary>
        /// <param name="command">Входная строка</param>
        /// <returns>True, если входная строка является командой</returns>
        public bool IsCommand(string command)
        {
            ExceptionHelper.ThrowIfNull(commandRegex, "commandRegex");
            return commandRegex.IsMatch(command.ToLower());
        }

        /// <summary>
        /// Проверить команду на возможность выполнения
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="chat">Чат</param>
        /// <param name="errorMessage">Сообщение об ошибке</param>
        /// <returns>True, если команду можно выполнить; Иначе False</returns>
        private bool CanExecuteCommand(IBotCommand command, Chat chat, out string errorMessage)
        {
            var attributes = command.GetType().GetCustomAttributes(false);
            foreach (CommandChatTypeAttribute commandChatType in attributes)
            {
                if (commandChatType.ChatTypes.All(i => i != chat.TelegramChatType))
                {
                    errorMessage = $"Данную команду можно использовать в следующих чатах: {string.Join(',', commandChatType.ChatTypes.Select(i => i.GetFullName()))}";
                    return false;
                }
                else
                {
                    //  ничего не делаем
                }
            }

            errorMessage = string.Empty;
            return true;
        }


        /// <summary>
        /// Обработать текстовую команду
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="chat">Чат</param>
        /// <param name="commandText">Текст команды</param>
        /// <returns>Task</returns>
        public async Task HandleTextCommand(User user, Chat chat, string command)
        {
            if (ContainsCommand(command))
            {
                var botCommand = GetCommand(command);
                if (CanExecuteCommand(botCommand, chat, out string errorMessage))
                {
                    await botCommand.ExecuteAsync(user, chat);
                }
                else
                {
                    await telegramBotClient.SendTextMessageAsync(chat.TelegramId, errorMessage);
                    throw new OperationCanceledException(errorMessage);
                }
            }
            else
            {
                await telegramBotClient.SendTextMessageAsync(chat.TelegramId, $"Неизвестная команда: {command}");
                throw new ArgumentException($"Unknown command: {command}");
            }
        }
    }
}