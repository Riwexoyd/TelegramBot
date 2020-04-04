using System;
using System.Collections.Generic;

namespace AspNetCoreTelegramBot.Models
{
    /// <summary>
    /// Ключевое слово
    /// </summary>
    public class Keyword
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Слово, максимальная длина 50 символов
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// Автор (создатель) слова
        /// </summary>
        public User Author { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Коллекция цитат
        /// </summary>
        public ICollection<QuoteKeyword> QuoteKeywords { get; set; }

        public Keyword()
        {
            QuoteKeywords = new List<QuoteKeyword>();
        }
    }
}