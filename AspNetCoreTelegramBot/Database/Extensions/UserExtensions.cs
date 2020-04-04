using AspNetCoreTelegramBot.Helpers;
using AspNetCoreTelegramBot.Models;

using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

using TelegramUser = Telegram.Bot.Types.User;

namespace AspNetCoreTelegramBot.Database.Extensions
{
    public static class UserExtensions
    {
        /// <summary>
        /// Получить пользователя из бд по Telegram Id
        /// </summary>
        /// <param name="applicationContext">Контекст базы данных</param>
        /// <param name="id">Telegram ID</param>
        /// <returns>User, если найден, null </returns>
        public static async Task<User> FindUserByTelegramId(this ApplicationContext applicationContext, int id)
        {
            var user = await applicationContext.Users
                .FirstOrDefaultAsync(i => i.TelegramId == id)
                .ConfigureAwait(false);
            return user;
        }

        /// <summary>
        /// Получить объект пользователя из объекта телеграм пользователя
        /// </summary>
        /// <param name="applicationContext">Контекст базы данных</param>
        /// <param name="telegramUser">Объект пользователя телеграма</param>
        /// <returns>Пользователь</returns>
        public static async Task<User> GetUserFromTelegramModel(this ApplicationContext applicationContext, TelegramUser telegramUser)
        {
            ExceptionHelper.ThrowIfNull(telegramUser, "telegramUser");
            var user = await applicationContext.FindUserByTelegramId(telegramUser.Id);
            if (user == null)
            {
                user = new User()
                {
                    TelegramId = telegramUser.Id,
                    FirstName = telegramUser.FirstName,
                    LastName = telegramUser.LastName,
                    Username = telegramUser.Username
                };

                await applicationContext.Users
                    .AddAsync(user)
                    .ConfigureAwait(false);

                await applicationContext.SaveChangesAsync();
            }

            return user;
        }
    }
}