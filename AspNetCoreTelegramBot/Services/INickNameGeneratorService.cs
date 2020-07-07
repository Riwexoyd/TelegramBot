using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreTelegramBot.Services
{
    /// <summary>
    /// Генератор ников
    /// </summary>
    public interface INickNameGeneratorService
    {
        /// <summary>
        /// Сгенерировать ник
        /// </summary>
        /// <returns>Сгенерированный ник</returns>
        Task<string> GenerateNickName();

        /// <summary>
        /// Получить информацию о нике
        /// </summary>
        /// <param name="nickName">Ник</param>
        /// <returns>Информация</returns>
        Task<string> GetNickNameInformation(string nickName);

        /// <summary>
        /// Инициализировать генератор
        /// </summary>
        /// <returns>Task</returns>
        Task InitializeGenerator();
    }
}
