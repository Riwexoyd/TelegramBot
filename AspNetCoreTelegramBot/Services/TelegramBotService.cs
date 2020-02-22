using AspNetCoreTelegramBot.Managers;

using Telegram.Bot;

using TelegramUser = Telegram.Bot.Types.User;

namespace AspNetCoreTelegramBot.Services
{
    public class TelegramBotService : ITelegramBotService
    {
        public ITelegramBotClient TelegramBotClient { get; }

        public TelegramUser BotUser { get; }

        public ICommandManager CommandManager { get; }

        public TelegramBotService(ITelegramBotClient telegramBotClient)
        {
            TelegramBotClient = telegramBotClient;
            BotUser = telegramBotClient.GetMeAsync().Result;
            CommandManager = new CommandManager(this);
        }
    }
}