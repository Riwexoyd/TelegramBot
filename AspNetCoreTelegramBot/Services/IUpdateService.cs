using System.Threading.Tasks;

using Telegram.Bot.Types;

namespace AspNetCoreTelegramBot.Services
{
    /// <summary>
    /// Сервис обработки обновлений из телеграма
    /// </summary>
    public interface IUpdateService
    {
        /// <summary>
        /// Обработать обновление
        /// </summary>
        /// <param name="update">Входящее обновление</param>
        /// <returns></returns>
        Task HandleUpdateAsync(Update update);
    }
}