using System;
using System.Collections.Generic;

namespace AspNetCoreTelegramBot.Models
{
    /// <summary>
    /// Цитаты-ключевые слова
    /// </summary>
    public class QuoteKeyword : IEquatable<QuoteKeyword>
    {
        /// <summary>
        /// Идентификатор цитаты
        /// </summary>
        public int QuoteId { get; set; }

        /// <summary>
        /// Цитата
        /// </summary>
        public Quote Quote { get; set; }

        /// <summary>
        /// Идентификатор ключевого слова
        /// </summary>
        public int KeywordId { get; set; }

        /// <summary>
        /// Ключевое слово
        /// </summary>
        public Keyword Keyword { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as QuoteKeyword);
        }

        public bool Equals(QuoteKeyword other)
        {
            return other != null &&
                   QuoteId == other.QuoteId &&
                   KeywordId == other.KeywordId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(QuoteId, KeywordId);
        }

        public static bool operator ==(QuoteKeyword left, QuoteKeyword right)
        {
            return EqualityComparer<QuoteKeyword>.Default.Equals(left, right);
        }

        public static bool operator !=(QuoteKeyword left, QuoteKeyword right)
        {
            return !(left == right);
        }
    }
}