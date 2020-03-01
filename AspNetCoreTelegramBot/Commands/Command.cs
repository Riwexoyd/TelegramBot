using AspNetCoreTelegramBot.Database;
using AspNetCoreTelegramBot.Models;
using AspNetCoreTelegramBot.Services;

using System.Threading.Tasks;

namespace AspNetCoreTelegramBot.Commands
{
    internal abstract class Command : ICommand
    {
        protected ITelegramBotService TelegramBotService { get; }

        public Command(ITelegramBotService telegramBotService)
        {
            TelegramBotService = telegramBotService;
        }

        public abstract Task ExecuteAsync(ApplicationContext applicationContext, User sender, Chat chat);
    }
}