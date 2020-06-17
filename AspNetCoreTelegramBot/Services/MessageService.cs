using AspNetCoreTelegramBot.Database;
using AspNetCoreTelegramBot.Database.Extensions;
using AspNetCoreTelegramBot.Helpers;

using System;
using System.Threading.Tasks;

using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AspNetCoreTelegramBot.Services
{
    /// <summary>
    /// Сервис обработки сообщений телеграма
    /// </summary>
    internal class MessageService : IMessageService
    {
        private readonly ApplicationContext applicationContext;
        private readonly ICommandService commandService;
        private readonly ITextHandlerService textHandlerService;

        public MessageService(ApplicationContext applicationContext,
            ICommandService commandService,
            ITextHandlerService textHandlerService)
        {
            this.applicationContext = applicationContext;
            this.commandService = commandService;
            this.textHandlerService = textHandlerService;
        }

        /// <summary>
        /// Обработать входящее сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <returns></returns>
        public async Task HandleMessageAsync(Message message)
        {
            ExceptionHelper.ThrowIfNull(message, "message");

            //  обработка сообщения в зависимости от типа сообщения
            //  TODO: сюда же можно добавить обработчики остальных типов сообщений
            Func<Task> execute = message.Type switch
            {
                MessageType.Text => () => HandleTextMessageAsync(message),
                _ => () => Task.FromException(new NotSupportedException($"Unknown Message Type: {message.Type}"))
            };

            await execute();
        }

        /// <summary>
        /// Обработать входящее текстовое сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <returns></returns>
        private async Task HandleTextMessageAsync(Message message)
        {
            ExceptionHelper.ThrowIfNullOrEmpty(message.Text, "message.Text");

            //  получаем отправителя и чат
            var user = await applicationContext.GetUserFromTelegramModel(message.From);
            var chat = await applicationContext.GetChatFromTelegramModel(message.Chat);

            // инициализируем сервис команд
            await commandService.InitializeAsync();

            //  если проходит паттерн команды
            if (commandService.IsCommand(message.Text))
            {
                //  обрабатываем команду
                await commandService.HandleTextCommand(user, chat, message.Text);
            }
            else
            {
                //  обрабатываем текст
                await textHandlerService.HandleTextAsync(user, chat, message.Text);
            }
        }
    }
}