using AspNetCoreTelegramBot.Models;

using System;
using System.Collections.Generic;

namespace AspNetCoreTelegramBot.Services
{
    public class AuthService : IAuthService
    {
        private Dictionary<User, (string, DateTime)> userAuthCodes = new Dictionary<User, (string, DateTime)>();
        private readonly Random random = new Random();

        public string GenerateCode(User user)
        {
            if (userAuthCodes.ContainsKey(user))
            {
                userAuthCodes.Remove(user);
            }

            var userData = (random.Next(1000, 10000).ToString(), DateTime.UtcNow.AddMinutes(3));
            userAuthCodes.Add(user, userData);
            return userData.Item1;
        }

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