using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;

namespace AspNetCoreTelegramBot.Models
{
    /// <summary>
    /// Цитата или фраза
    /// </summary>
    public class Quote
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Текст цитаты
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Автор цитаты
        /// </summary>
        public User Author { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Коллекция ключевых слов
        /// </summary>
        public ICollection<QuoteKeyword> QuoteKeywords { get; set; }

        public Quote()
        {
            QuoteKeywords = new List<QuoteKeyword>();
        }
    }

    public class QuoteConfiguration : IEntityTypeConfiguration<Quote>
    {
        public void Configure(EntityTypeBuilder<Quote> builder)
        {
            builder
                .HasKey(i => i.Id);

            builder
                .Property(i => i.CreationDate)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("now()");
        }
    }
}