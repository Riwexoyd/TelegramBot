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
        /// <summary>
        /// Дата защиты диплома
        /// </summary>
        private readonly DateTime Defense = new DateTime(2020, 07, 6);

        protected ITelegramBotClient TelegramBotClient { get; }

        protected ApplicationContext ApplicationContext { get; }

        public DeadLineCommand(ITelegramBotClient telegramBotClient, ApplicationContext applicationContext)
        {
            TelegramBotClient = telegramBotClient;
            ApplicationContext = applicationContext;
        }

        public async Task ExecuteAsync(User sender, Chat chat)
        {
            var currentDate = DateTime.Now.Date;
            var defenseTimeSpan = Defense - currentDate;

            await TelegramBotClient.SendTextMessageAsync(chat.TelegramId, $"До защиты: {defenseTimeSpan.Days} дней");
        }
    }
}
