using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreTelegramBot.Models.Configurations
{
    public class RouletteWinnerConfiguration : IEntityTypeConfiguration<RouletteWinner>
    {
        public void Configure(EntityTypeBuilder<RouletteWinner> builder)
        {
            builder
                .HasKey(i => i.Id);

            builder
                .HasIndex(i => i.Date);

            builder
                .Property(i => i.Date)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("now() at time zone 'utc'");
        }
    }
}