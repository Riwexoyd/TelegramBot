using System.Threading.Tasks;

using Telegram.Bot.Types;

namespace AspNetCoreTelegramBot.Services
{
    /// <summary>
    /// Обработчик сообщений телеграма
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// Обработать сообщение асинхронно
        /// </summary>
        /// <param name="message">Входящее сообщение</param>
        /// <returns></returns>
        Task HandleMessageAsync(Message message);
    }
}