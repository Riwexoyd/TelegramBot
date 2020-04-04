using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreTelegramBot.Models.Configurations
{
    public class RouletteCategoryConfiguration : IEntityTypeConfiguration<RouletteCategory>
    {
        public void Configure(EntityTypeBuilder<RouletteCategory> builder)
        {
            builder
                .HasKey(i => i.Id);

            builder
                .Property(i => i.CreationDate)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("now() at time zone 'utc'");

            builder
                .Property(i => i.Title)
                .IsRequired(true)
                .HasMaxLength(50);

            builder
                .HasData(new RouletteCategory
                {
                    Id = 1,
                    IsPublic = true,
                    Title = "Красавчик дня"
                });
        }
    }
}