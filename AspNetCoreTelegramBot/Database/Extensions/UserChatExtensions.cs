using AspNetCoreTelegramBot.Models;

using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

namespace AspNetCoreTelegramBot.Database.Extensions
{
    /// <summary>
    /// Расширения для сущности UserChat
    /// </summary>
    public static class UserChatExtensions
    {
        /// <summary>
        /// Получить чат пользователя
        /// </summary>
        /// <param name="applicationContext">Контекст базы данных</param>
        /// <param name="user">Пользователь</param>
        /// <param name="chat">Чат</param>
        /// <returns>Чат пользователя</returns>
        public static async Task<UserChat> GetUserChatAsync(this ApplicationContext applicationContext, User user, Chat chat)
        {
            var userChat = await applicationContext.UserChats.FirstOrDefaultAsync(i => i.User == user && i.Chat == chat);
            if (userChat == null)
            {
                userChat = new UserChat
                {
                    ChatId = chat.Id,
                    UserId = user.Id
                };
                applicationContext.UserChats.Add(userChat);
                await applicationContext.SaveChangesAsync();
            }

            return userChat;
        }
    }
}