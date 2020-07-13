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
        /// <param name="chars">Символы для начала</param>
        /// <returns>Сгенерированный ник</returns>
        string GenerateNickName(char[] chars);

        /// <summary>
        /// Получить информацию о нике
        /// </summary>
        /// <param name="nickName">Ник</param>
        /// <returns>Информация</returns>
        string GetNickNameInformation(string nickName);

        /// <summary>
        /// Инициализировать генератор
        /// </summary>
        /// <returns>Task</returns>
        Task InitializeGenerator();
    }
}