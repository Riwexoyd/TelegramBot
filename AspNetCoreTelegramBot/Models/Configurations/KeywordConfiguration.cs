using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreTelegramBot.Models.Configurations
{
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
                .HasDefaultValueSql("now() at time zone 'utc'");
        }
    }
}