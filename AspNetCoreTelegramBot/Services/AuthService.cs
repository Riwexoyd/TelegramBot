using AspNetCoreTelegramBot.Models;

using System;
using System.Collections.Generic;

namespace AspNetCoreTelegramBot.Services
{
    /// <summary>
    /// Сервис аутентификации, хранит временные пароли для входа
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly Dictionary<User, (string, DateTime)> userAuthCodes = new Dictionary<User, (string, DateTime)>();
        private readonly Random random = new Random();

        /// <summary>
        /// Сгенерировать код для пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>Сгенерированный код</returns>
        public string GenerateCode(User user)
        {
            if (userAuthCodes.ContainsKey(user))
            {
                userAuthCodes.Remove(user);
            }

            //  TODO: вынести время в конфигурацию
            var userData = (random.Next(1000, 10000).ToString(), DateTime.UtcNow.AddMinutes(3));
            userAuthCodes.Add(user, userData);
            return userData.Item1;
        }

        /// <summary>
        /// Проверить корректность кода
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="code">Код</param>
        /// <returns>True, если код корректен; Иначе False</returns>
        public bool IsCorrectCode(User user, string code)
        {
            if (!userAuthCodes.TryGetValue(user, out (string authCode, DateTime expirationDate) userData))
            {
                return false;
            }

            if (userData.authCode != code)
            {
                return false;
            }

            if (DateTime.UtcNow > userData.expirationDate)
            {
                return false;
            }

            return true;
        }
    }
}