using AspNetCoreTelegramBot.Attributes;
using AspNetCoreTelegramBot.Commands;
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
    public class CommandService : ICommandService
    {
        private const string CommandPostfix = "command";
        private readonly Dictionary<string, IBotCommand> commandDictionary;
        private readonly ITelegramBotClient telegramBotClient;

        private Regex commandRegex;

        public CommandService(ITelegramBotClient telegramBotClient, IEnumerable<IBotCommand> commands)
        {
            this.telegramBotClient = telegramBotClient;

            commandDictionary = commands.ToDictionary(i =>
            {
                string name = i.GetType().Name.ToLower();
                name = name.EndsWith(CommandPostfix) ? name.Remove(name.Length - CommandPostfix.Length) : name;
                return name;
            },
            j => j);
        }

        public async Task InitializeAsync()
        {
            var bot = await telegramBotClient.GetMeAsync();
            //  TODO: переписать регекс
            commandRegex = new Regex($@"^\/[A-Za-z]+(@{bot.Username})?", RegexOptions.IgnoreCase);
        }

        public bool ContainsCommand(string command)
        {
            ExceptionHelper.ThrowIfNull(commandRegex, "commandRegex");
            return commandDictionary.ContainsKey(ParseCommand(command));
        }

        public IBotCommand GetCommand(string command)
        {
            if (!ContainsCommand(command))
            {
                throw new ArgumentException("Invalid command");
            }

            return commandDictionary[ParseCommand(command)];
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
        public bool CanExecuteCommand(IBotCommand command, Chat chat, out string errorMessage)
        {
            var attributes = command.GetType().GetCustomAttributes(false);
            foreach (CommandChatTypeAttribute commandChatType in attributes)
            {
                if (commandChatType.ChatTypes.All(i => i != chat.TelegramChatType))
                {
                    //  TODO: уточнить, где команду можно применять
                    errorMessage = "Данную команду нельзя использовать в этом чате";
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
    }
}