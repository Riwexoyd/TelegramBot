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
        private readonly ITelegramBotClient telegramBotClient;

        private readonly ApplicationContext applicationContext;

        private readonly INickNameGeneratorService nickNameGeneratorService;

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
            var nickName = nickNameGeneratorService.GenerateNickName(null);
            var info = nickNameGeneratorService.GetNickNameInformation(nickName);
            await telegramBotClient.SendTextMessageAsync(chat.TelegramId, $"Ник: {nickName}\n{info}");
        }
    }
}