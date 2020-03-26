using AspNetCoreTelegramBot.Managers;

using Microsoft.Extensions.Configuration;

using Telegram.Bot;

using TelegramUser = Telegram.Bot.Types.User;

namespace AspNetCoreTelegramBot.Services
{
    public class TelegramBotService : ITelegramBotService
    {
        public ITelegramBotClient TelegramBotClient { get; }

        public TelegramUser BotUser { get; }

        public ICommandManager CommandManager { get; }

        public IConfiguration Configuration { get; }

        public TelegramBotService(ITelegramBotClient telegramBotClient, IConfiguration configuration)
        {
            TelegramBotClient = telegramBotClient;
            BotUser = telegramBotClient.GetMeAsync().Result;
            CommandManager = new CommandManager(this);
            Configuration = configuration;
        }
    }
}