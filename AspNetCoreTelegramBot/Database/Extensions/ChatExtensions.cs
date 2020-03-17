using AspNetCoreTelegramBot.Helpers;
using AspNetCoreTelegramBot.Models;

using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

using TelegramChat = Telegram.Bot.Types.Chat;

namespace AspNetCoreTelegramBot.Database.Extensions
{
    public static class ChatExtensions
    {
        /// <summary>
        /// Найти чат по id телеграма
        /// </summary>
        /// <param name="applicationContext">Контекст базы данных</param>
        /// <param name="id">Telegram ID</param>
        /// <returns>Объект - если найден, null - если не найден</returns>
        public static async Task<Chat> FindChatByTelegramId(this ApplicationContext applicationContext, long id)
        {
            var chat = await applicationContext.Chats
                .FirstOrDefaultAsync(i => i.TelegramId == id)
                .ConfigureAwait(false);
            return chat;
        }

        /// <summary>
        /// Получить объект чата по объекту телеграма
        /// </summary>
        /// <param name="applicationContext">Контекст базы данных</param>
        /// <param name="telegramChat">Объект телеграма</param>
        /// <returns>Объект чата</returns>
        public static async Task<Chat> GetChatFromTelegramModel(this ApplicationContext applicationContext, TelegramChat telegramChat)
        {
            ExceptionHelper.ThrowIfNull(telegramChat, "telegramChat");
            var chat = await applicationContext.FindChatByTelegramId(telegramChat.Id);
            if (chat == null)
            {
                chat = new Chat()
                {
                    TelegramId = telegramChat.Id,
                    FirstName = telegramChat.FirstName,
                    LastName = telegramChat.LastName,
                    Username = telegramChat.Username,
                    Title = telegramChat.Title,
                    Description = telegramChat.Description,
                    TelegramChatType = telegramChat.Type
                };

                await applicationContext.Chats
                    .AddAsync(chat)
                    .ConfigureAwait(false);

                await applicationContext.SaveChangesAsync();
            }

            return chat;
        }
    }
}