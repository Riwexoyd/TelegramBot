using AspNetCoreTelegramBot.Database;
using AspNetCoreTelegramBot.Models;

using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

using Telegram.Bot;

namespace AspNetCoreTelegramBot.Commands
{
    internal class TestCommand : BotCommand
    {
        protected override ITelegramBotClient TelegramBotClient { get; }

        protected override ApplicationContext ApplicationContext { get; }

        public TestCommand(ITelegramBotClient telegramBotClient, ApplicationContext applicationContext)
        {
            TelegramBotClient = telegramBotClient;
            ApplicationContext = applicationContext;
        }

        public override async Task ExecuteAsync(User sender, Chat chat)
        {
            var me = await TelegramBotClient.GetMeAsync();
            await TelegramBotClient.SendTextMessageAsync(chat.TelegramId, $"Bot Info: {me.FirstName} {me.LastName} {me.Username}");
            var userCount = await ApplicationContext.Users.CountAsync().ConfigureAwait(false);
            var chatCount = await ApplicationContext.Chats.CountAsync().ConfigureAwait(false);
            await TelegramBotClient.SendTextMessageAsync(chat.TelegramId, $"User Count: {userCount}, Chat Count: {chatCount}");
        }
    }
}