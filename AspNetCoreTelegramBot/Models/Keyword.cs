using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

    public class KeywordConfiguration : IEntityTypeConfiguration<Keyword>
    {
        public void Configure(EntityTypeBuilder<Keyword> builder)
        {
            builder
                .HasKey(i => i.Id);

            builder
                .HasIndex(i => i.Word)
                .IsUnique(true);

            builder
                .Property(i => i.Word)
                .HasMaxLength(50);

            builder
                .Property(i => i.CreationDate)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("now()");
        }
    }
}