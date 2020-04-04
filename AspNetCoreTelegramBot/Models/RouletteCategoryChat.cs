using System;
using System.Collections.Generic;

namespace AspNetCoreTelegramBot.Models
{
    /// <summary>
    /// Чат для категории рулетки
    /// </summary>
    public class RouletteCategoryChat : IEquatable<RouletteCategoryChat>
    {
        /// <summary>
        /// Чат
        /// </summary>
        public Chat Chat { get; set; }

        /// <summary>
        /// Чат Id
        /// </summary>
        public int ChatId { get; set; }

        /// <summary>
        /// Категория
        /// </summary>
        public RouletteCategory RouletteCategory { get; set; }

        /// <summary>
        /// Id категории
        /// </summary>
        public int RouletteCategoryId { get; set; }

        /// <summary>
        /// Победители
        /// </summary>
        public ICollection<RouletteWinner> Winners { get; set; }

        public RouletteCategoryChat()
        {
            Winners = new List<RouletteWinner>();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as RouletteCategoryChat);
        }

        public bool Equals(RouletteCategoryChat other)
        {
            return other != null &&
                   ChatId == other.ChatId &&
                   RouletteCategoryId == other.RouletteCategoryId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ChatId, RouletteCategoryId);
        }

        public static bool operator ==(RouletteCategoryChat left, RouletteCategoryChat right)
        {
            return EqualityComparer<RouletteCategoryChat>.Default.Equals(left, right);
        }

        public static bool operator !=(RouletteCategoryChat left, RouletteCategoryChat right)
        {
            return !(left == right);
        }
    }
}