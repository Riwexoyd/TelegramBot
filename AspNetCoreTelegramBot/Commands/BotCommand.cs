using AspNetCoreTelegramBot.Database;
using AspNetCoreTelegramBot.Models;

using System.Threading.Tasks;

using Telegram.Bot;

namespace AspNetCoreTelegramBot.Commands
{
    /// <summary>
    /// Абстрактный класс команды
    /// </summary>
    internal abstract class BotCommand : IBotCommand
    {
        /// <summary>
        /// Клиент телеграм бота
        /// </summary>
        protected abstract ITelegramBotClient TelegramBotClient { get; }

        /// <summary>
        /// Контекст базы данных
        /// </summary>
        protected abstract ApplicationContext ApplicationContext { get; }

        public abstract Task ExecuteAsync(User sender, Chat chat);
    }
}