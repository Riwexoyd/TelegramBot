using AspNetCoreTelegramBot.Database;
using AspNetCoreTelegramBot.Database.Extensions;
using AspNetCoreTelegramBot.Helpers;

using System;
using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AspNetCoreTelegramBot.Services
{
    /// <summary>
    /// Сервис обработки обновлений
    /// </summary>
    public class UpdateService : IUpdateService
    {
        private ApplicationContext applicationContext;
        private ITelegramBotClient telegramBotClient;
        private ICommandService commandService;
        private ITextHandlerService textHandlerService;

        public UpdateService(ApplicationContext applicationContext,
            ITelegramBotClient telegramBotClient,
            ICommandService commandService,
            ITextHandlerService textHandlerService)
        {
            this.applicationContext = applicationContext;
            this.telegramBotClient = telegramBotClient;
            this.commandService = commandService;
            this.textHandlerService = textHandlerService;
        }

        /// <summary>
        /// Асинхронно обработать обновление
        /// </summary>
        /// <param name="update">Входящее обновление</param>
        /// <returns></returns>
        public async Task HandleUpdateAsync(Update update)
        {
            if (update != null)
            {
                //  операция в зависимости от типа обновления
                Func<Task> execute = update.Type switch
                {
                    UpdateType.Message => () => CheckMessageAsync(update.Message),
                    _ => () => Task.FromException(new NotSupportedException())
                };
                //  инициализируем сервисы
                await InitializeAsync();
                //  пробуем обработать исключение
                try
                {
                    await execute();
                }
                catch (Exception e)
                {
                    //  TODO: сюда логгер
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                //  TODO: сюда тоже логгер
                Console.WriteLine("Update IS NULL");
            }
        }

        /// <summary>
        /// Инициализировать необходимые сервисы
        /// </summary>
        /// <returns></returns>
        private async Task InitializeAsync()
        {
            await commandService.InitializeAsync();
        }

        /// <summary>
        /// Проверить сообщение
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task CheckMessageAsync(Message message)
        {
            ExceptionHelper.ThrowIfNull(message, "message");
            ExceptionHelper.ThrowIfNullOrEmpty(message.Text, "message.Text");

            //  получаем отправителя и чат
            var user = await applicationContext.GetUserFromTelegramModel(message.From);
            var chat = await applicationContext.GetChatFromTelegramModel(message.Chat);

            //  если проходит паттерн команды
            if (commandService.IsCommand(message.Text))
            {
                if (commandService.ContainsCommand(message.Text))
                {
                    var command = commandService.GetCommand(message.Text);
                    if (commandService.CanExecuteCommand(command, chat, out string errorMessage))
                    {
                        await command.ExecuteAsync(user, chat);
                    }
                    else
                    {
                        await telegramBotClient.SendTextMessageAsync(chat.TelegramId, errorMessage);
                        throw new OperationCanceledException(errorMessage);
                    }
                }
                else
                {
                    string unknownCommandMessage = $"Unknown command: {message.Text}";
                    await telegramBotClient.SendTextMessageAsync(chat.TelegramId, unknownCommandMessage);
                    throw new ArgumentException(unknownCommandMessage);
                }
            }
            else
            {
                //  обрабатываем текст
                await textHandlerService.HandleTextAsync(user, chat, message.Text);
            }
        }
    }
}