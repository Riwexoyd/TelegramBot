using AspNetCoreTelegramBot.Models;
using AspNetCoreTelegramBot.Services;

using System.Linq;
using System.Threading.Tasks;

using Telegram.Bot;

namespace AspNetCoreTelegramBot.TextHandlers
{
    /// <summary>
    /// Обработчик гусей
    /// </summary>
    public class GooseTextHandler : WordTextHandler
    {
        private const string Goose = "гус";

        private readonly ITelegramBotClient telegramBotClient;
        private readonly IGooseService gooseService;

        public GooseTextHandler(ITelegramBotClient telegramBotClient, IGooseService gooseService)
        {
            this.telegramBotClient = telegramBotClient;
            this.gooseService = gooseService;
        }

        public override async Task<bool> HandleAsync(User sender, Chat chat, string text)
        {
            var words = GetWordsFromText(text);
            if (words.Any(i => i.ToLower().StartsWith(Goose)))
            {
                await telegramBotClient.SendTextMessageAsync(chat.TelegramId, gooseService.GetRandomGoose());
                return true;
            }

            return false;
        }
    }
}