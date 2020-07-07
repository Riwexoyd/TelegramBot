using AspNetCoreTelegramBot.Database;
using AspNetCoreTelegramBot.Models;
using AspNetCoreTelegramBot.Services;
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

        private INickNameGeneratorService nickNameGeneratorService;

        public GenerateNickName(ITelegramBotClient telegramBotClient, 
            ApplicationContext applicationContext, 
            INickNameGeneratorService nickNameGeneratorService)
        {
            this.telegramBotClient = telegramBotClient;
            this.applicationContext = applicationContext;
            this.nickNameGeneratorService = nickNameGeneratorService;
        }

        public async Task ExecuteAsync(User sender, Chat chat)
        {
            await nickNameGeneratorService.GenerateNickName();
            await telegramBotClient.SendTextMessageAsync(chat.TelegramId, "В разработке...");
        }
    }
}