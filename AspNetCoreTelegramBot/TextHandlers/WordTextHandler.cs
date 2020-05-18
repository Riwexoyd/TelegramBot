using AspNetCoreTelegramBot.Models;

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AspNetCoreTelegramBot.TextHandlers
{
    public abstract class WordTextHandler : ITextHandler
    {
        /// <summary>
        /// Минимальная длина слова
        /// </summary>
        private const int MinWordLength = 3;

        /// <summary>
        /// Регекс для получения слов из текста
        /// </summary>
        private static readonly Regex wordRegex = new Regex(@"[A-Za-zА-Яа-я]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        protected static IEnumerable<string> GetWordsFromText(string text)
        {
            return wordRegex.Matches(text)
                .Select(i => i.Value.ToLower())
                .Where(i => i.Length >= MinWordLength)
                .Distinct()
                .ToList();
        }

        public abstract Task<bool> HandleAsync(User sender, Chat chat, string text);
    }
}