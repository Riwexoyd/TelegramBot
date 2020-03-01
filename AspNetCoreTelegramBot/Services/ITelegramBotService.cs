using AspNetCoreTelegramBot.Managers;

using Telegram.Bot;
using Telegram.Bot.Types;

namespace AspNetCoreTelegramBot.Services
{
    public interface ITelegramBotService
    {
        /// <summary>
        /// Клиент телеграм бота
        /// </summary>
        ITelegramBotClient TelegramBotClient { get; }

        /// <summary>
        /// Менеджер для работы с командами
        /// </summary>
        ICommandManager CommandManager { get; }

        /// <summary>
        /// Telegram User бот-а
        /// </summary>
        User BotUser { get; }
    }
}