using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreTelegramBot.Models.Configurations
{
    public class QuoteConfiguration : IEntityTypeConfiguration<Quote>
    {
        public void Configure(EntityTypeBuilder<Quote> builder)
        {
            builder
                .HasKey(i => i.Id);

            builder
                .Property(i => i.CreationDate)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("now() at time zone 'utc'");
        }
    }
}