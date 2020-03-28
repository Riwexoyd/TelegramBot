using AspNetCoreTelegramBot.Models;

using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTelegramBot.Database
{
    public class ApplicationContext : DbContext
    {
        /// <summary>
        /// Пользователи
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Чаты
        /// </summary>
        public DbSet<Chat> Chats { get; set; }

        /// <summary>
        /// Цитаты
        /// </summary>
        public DbSet<Quote> Quotes { get; set; }

        /// <summary>
        /// Ключевые слова
        /// </summary>
        public DbSet<Keyword> Keywords { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ChatConfiguration());
            modelBuilder.ApplyConfiguration(new QuoteConfiguration());
            modelBuilder.ApplyConfiguration(new KeywordConfiguration());
            modelBuilder.ApplyConfiguration(new QuoteKeywordConfiguration());
        }
    }
}