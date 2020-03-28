using Microsoft.Extensions.Logging;

using System;
using System.Threading.Tasks;

using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AspNetCoreTelegramBot.Services
{
    /// <summary>
    /// Сервис обработки обновлений
    /// </summary>
    public class UpdateService : IUpdateService
    {
        private readonly ILogger<UpdateService> logger;
        private readonly ICallbackQueryService callbackQueryService;
        private readonly IMessageService messageHandlerService;

        public UpdateService(IMessageService messageHandlerService,
            ILogger<UpdateService> logger,
            ICallbackQueryService callbackQueryService
            )
        {
            this.logger = logger;
            this.callbackQueryService = callbackQueryService;
            this.messageHandlerService = messageHandlerService;
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
                    UpdateType.Message => () => messageHandlerService.HandleMessageAsync(update.Message),
                    UpdateType.CallbackQuery => () => callbackQueryService.HandleCallbackQueryAsync(update.CallbackQuery),
                    _ => () => Task.FromException(new NotSupportedException($"Unknown Update Type: {update.Type}"))
                };

                //  пробуем обработать исключение
                try
                {
                    await execute();
                }
                catch (Exception e)
                {
                    logger.LogError(e.StackTrace);
                }
            }
            else
            {
                logger.LogWarning("Update was Null");
            }
        }
    }
}