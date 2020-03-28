using AspNetCoreTelegramBot.Database;
using AspNetCoreTelegramBot.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Telegram.Bot;

namespace AspNetCoreTelegramBot.TextHandlers
{
    /// <summary>
    /// Обработчик текста для ответа цитатами
    /// </summary>
    public class QuoteTextHandler : ITextHandler
    {
        /// <summary>
        /// Минимальная длина слова
        /// </summary>
        private const int MinWordLength = 3;

        /// <summary>
        /// Регекс для получения слов из текста
        /// </summary>
        private static readonly Regex wordRegex = new Regex(@"[A-Za-zА-Яа-я]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private readonly ITelegramBotClient telegramBotClient;
        private readonly ApplicationContext applicationContext;

        public QuoteTextHandler(ITelegramBotClient telegramBotClient, ApplicationContext applicationContext)
        {
            this.telegramBotClient = telegramBotClient;
            this.applicationContext = applicationContext;
        }

        public async Task<bool> HandleAsync(User sender, Chat chat, string text)
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

        private static IEnumerable<string> GetWordsFromText(string text)
        {
            return wordRegex.Matches(text)
                .Select(i => i.Value.ToLower())
                .Where(i => i.Length >= MinWordLength)
                .Distinct()
                .ToList();
        }
    }
}