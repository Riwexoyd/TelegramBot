using AspNetCoreTelegramBot.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreTelegramBot.CallbackQueries
{
    /// <summary>
    /// Callback запрос
    /// </summary>
    public interface ICallbackQuery
    {
        /// <summary>
        /// Обработать callback запрос
        /// </summary>
        /// <param name="data">Данные</param>
        /// <returns></returns>
        Task ExecuteQueryAsync(User sender, Chat chat, Dictionary<string, object> data);
    }
}