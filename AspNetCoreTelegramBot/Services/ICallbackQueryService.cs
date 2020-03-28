using AspNetCoreTelegramBot.CallbackQueries;

using System.Threading.Tasks;

using Telegram.Bot.Types;

using User = AspNetCoreTelegramBot.Models.User;

namespace AspNetCoreTelegramBot.Services
{
    /// <summary>
    /// Сервис обработки CallbackQuery
    /// </summary>
    public interface ICallbackQueryService
    {
        /// <summary>
        /// Обработать запрос
        /// </summary>
        /// <param name="callbackQuery">Запрос</param>
        /// <returns></returns>
        Task HandleCallbackQueryAsync(CallbackQuery callbackQuery);

        /// <summary>
        /// Создать строку запроса
        /// </summary>
        /// <typeparam name="T">Тип запроса</typeparam>
        /// <param name="user">Создатель запроса</param>
        /// <param name="args">Аргументы</param>
        /// <returns>Строка запроса</returns>
        string CreateQuery<T>(User user, params (string key, object value)[] args) where T : ICallbackQuery;
    }
}