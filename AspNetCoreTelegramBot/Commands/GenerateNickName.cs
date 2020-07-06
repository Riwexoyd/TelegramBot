using AspNetCoreTelegramBot.Database;
using AspNetCoreTelegramBot.Models;

using System.ComponentModel;
using System.Threading.Tasks;

using Telegram.Bot;

namespace AspNetCoreTelegramBot.Commands
{
    /// <summary>
    /// Сгенерировать nickname
    /// </summary>
    [Description("Сгенерировать NickName")]
    public class GenerateNickName : IBotCommand
    {
        private ITelegramBotClient telegramBotClient;

        private ApplicationContext applicationContext;

        public GenerateNickName(ITelegramBotClient telegramBotClient, ApplicationContext applicationContext)
        {
            this.telegramBotClient = telegramBotClient;
            this.applicationContext = applicationContext;
        }

        public async Task ExecuteAsync(User sender, Chat chat)
        {
            await telegramBotClient.SendTextMessageAsync(chat.TelegramId, "В разработке...");
        }
    }
}