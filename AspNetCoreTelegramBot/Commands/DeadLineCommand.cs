using AspNetCoreTelegramBot.Database;
using AspNetCoreTelegramBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace AspNetCoreTelegramBot.Commands
{
    /// <summary>
    /// Дэдлайн
    /// </summary>
    internal class DeadLineCommand : IBotCommand
    {
        protected ITelegramBotClient TelegramBotClient { get; }

        protected ApplicationContext ApplicationContext { get; }

        public DeadLineCommand(ITelegramBotClient telegramBotClient, ApplicationContext applicationContext)
        {
            TelegramBotClient = telegramBotClient;
            ApplicationContext = applicationContext;
        }

        public async Task ExecuteAsync(User sender, Chat chat)
        {
            await TelegramBotClient.SendTextMessageAsync(chat.TelegramId, $"Команда в разработке");
        }
    }
}
