using AspNetCoreTelegramBot.Models;

namespace AspNetCoreTelegramBot.Services
{
    /// <summary>
    /// Сервис аутентификации
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Сгенерировать код аутентификации для пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>Код</returns>
        string GenerateCode(User user);

        /// <summary>
        /// Проверка на корректность кода для пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        bool IsCorrectCode(User user, string code);
    }
}