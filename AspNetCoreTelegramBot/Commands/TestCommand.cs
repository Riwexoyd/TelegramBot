using AspNetCoreTelegramBot.Database;
using AspNetCoreTelegramBot.Services;

using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

namespace AspNetCoreTelegramBot.Commands
{
    internal class TestCommand : Command
    {
        public TestCommand(ITelegramBotService telegramBotService) : base(telegramBotService)
        {
        }

        public override async Task ExecuteAsync(ApplicationContext applicationContext, Models.User sender, Models.Chat chat)
        {
            await TelegramBotService.TelegramBotClient.SendTextMessageAsync(chat.TelegramId, $"{TelegramBotService.BotUser.FirstName} {TelegramBotService.BotUser.LastName} {TelegramBotService.BotUser.Username}");
            var userCount = await applicationContext.Users.CountAsync().ConfigureAwait(false);
            var chatCount = await applicationContext.Chats.CountAsync().ConfigureAwait(false);
            await TelegramBotService.TelegramBotClient.SendTextMessageAsync(chat.TelegramId, $"User Count: {userCount}, Chat Count: {chatCount}");
        }
    }
}