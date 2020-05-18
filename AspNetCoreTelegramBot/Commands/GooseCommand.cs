using AspNetCoreTelegramBot.Models;
using AspNetCoreTelegramBot.Services;

using System.Threading.Tasks;

using Telegram.Bot;

namespace AspNetCoreTelegramBot.Commands
{
    /// <summary>
    /// Команда гуся
    /// </summary>
    public class GooseCommand : IBotCommand
    {
        private readonly ITelegramBotClient telegramBotClient;
        private readonly IGooseService gooseService;

        public GooseCommand(ITelegramBotClient telegramBotClient, IGooseService gooseService)
        {
            this.telegramBotClient = telegramBotClient;
            this.gooseService = gooseService;
        }

        public async Task ExecuteAsync(User sender, Chat chat)
        {
            await telegramBotClient.SendTextMessageAsync(chat.TelegramId, gooseService.GetRandomGoose());
        }
    }
}