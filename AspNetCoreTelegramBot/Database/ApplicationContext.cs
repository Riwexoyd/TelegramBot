using AspNetCoreTelegramBot.Models;
using AspNetCoreTelegramBot.Models.Configurations;

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

        /// <summary>
        /// Ключевые слова с цитатами
        /// </summary>
        public DbSet<QuoteKeyword> QuoteKeywords { get; set; }

        /// <summary>
        /// Категории для победителя дня
        /// </summary>
        public DbSet<RouletteCategory> RouletteCategories { get; set; }

        /// <summary>
        /// Чаты пользователей
        /// </summary>
        public DbSet<UserChat> UserChats { get; set; }

        /// <summary>
        /// Победители по категориям
        /// </summary>
        public DbSet<RouletteWinner> RouletteWinners { get; set; }

        /// <summary>
        /// Музыка
        /// </summary>
        public DbSet<Music> Musics { get; set; }

        /// <summary>
        /// Категории по чатам
        /// </summary>
        public DbSet<RouletteCategoryChat> RouletteCategoryChats { get; set; }

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
            modelBuilder.ApplyConfiguration(new UserChatConfiguration());
            modelBuilder.ApplyConfiguration(new RouletteCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new RouletteCategoryChatConfiguration());
            modelBuilder.ApplyConfiguration(new RouletteWinnerConfiguration());
            modelBuilder.ApplyConfiguration(new MusicConfiguration());
        }
    }
}