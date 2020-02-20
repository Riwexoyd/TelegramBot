using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTelegramBot.Database
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

#if DEBUG

        public ApplicationContext()
        {
            //  Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql($"Server=localhost;Port=5432;User Id=postgres;Password=123456;Database=test;sslmode=Prefer;Trust Server Certificate=true");
        }

#endif

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  modelbinder.applyconfiguration
        }
    }
}