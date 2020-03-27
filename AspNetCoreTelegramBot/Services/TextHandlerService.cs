using AspNetCoreTelegramBot.Helpers;
using AspNetCoreTelegramBot.Models;
using AspNetCoreTelegramBot.TextHandlers;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreTelegramBot.Services
{
    /// <summary>
    /// Сервис обработки текстовых сообщений
    /// </summary>
    public class TextHandlerService : ITextHandlerService
    {
        private readonly IEnumerable<ITextHandler> textHandlers;

        public TextHandlerService(IEnumerable<ITextHandler> textHandlers)
        {
            this.textHandlers = textHandlers;
        }

        /// <summary>
        /// Асинхронно обработать текстовое сообщение
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <param name="chat">Чат</param>
        /// <param name="text">Текст</param>
        /// <returns></returns>
        public async Task HandleTextAsync(User sender, Chat chat, string text)
        {
            ExceptionHelper.ThrowIfNullOrEmpty(text, "text");
            ExceptionHelper.ThrowIfNull(sender, "sender");
            ExceptionHelper.ThrowIfNull(chat, "chat");

            foreach (var handler in textHandlers)
            {
                var result = await handler.HandleAsync(sender, chat, text);
                if (result)
                {
                    break;
                }
            }
        }
    }
}