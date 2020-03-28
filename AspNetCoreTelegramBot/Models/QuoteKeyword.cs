using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;

namespace AspNetCoreTelegramBot.Models
{
    /// <summary>
    /// Таблица для отношения многие-ко-многим
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

    public class QuoteKeywordConfiguration : IEntityTypeConfiguration<QuoteKeyword>
    {
        public void Configure(EntityTypeBuilder<QuoteKeyword> builder)
        {
            builder
                .HasKey(i => new { i.QuoteId, i.KeywordId });

            builder
                .HasOne(i => i.Quote)
                .WithMany(i => i.QuoteKeywords)
                .HasForeignKey(i => i.QuoteId);

            builder
                .HasOne(i => i.Keyword)
                .WithMany(i => i.QuoteKeywords)
                .HasForeignKey(i => i.KeywordId);
        }
    }
}