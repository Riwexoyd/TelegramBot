using AspNetCoreTelegramBot.Database;
using AspNetCoreTelegramBot.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Telegram.Bot;

namespace AspNetCoreTelegramBot.TextHandlers
{
    /// <summary>
    /// Обработчик текста для ответа цитатами
    /// </summary>
    public class QuoteTextHandler : WordTextHandler
    {
        private readonly ITelegramBotClient telegramBotClient;
        private readonly ApplicationContext applicationContext;

        public QuoteTextHandler(ITelegramBotClient telegramBotClient, ApplicationContext applicationContext)
        {
            this.telegramBotClient = telegramBotClient;
            this.applicationContext = applicationContext;
        }

        public override async Task<bool> HandleAsync(User sender, Chat chat, string text)
        {
            var words = GetWordsFromText(text);
            var quotes = await applicationContext.Keywords.Where(i => words.Contains(i.Word))
                .SelectMany(i => i.QuoteKeywords)
                .Select(i => i.Quote)
                .Distinct()
                .ToListAsync();

            if (!quotes.Any())
            {
                return false;
            }
            else
            {
                Random random = new Random();
                var index = random.Next(quotes.Count);
                await telegramBotClient.SendTextMessageAsync(chat.TelegramId, quotes[index].Text);
                return true;
            }
        }
    }
}