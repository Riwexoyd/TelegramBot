using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreTelegramBot.Models.Configurations
{
    public class RouletteCategoryChatConfiguration : IEntityTypeConfiguration<RouletteCategoryChat>
    {
        public void Configure(EntityTypeBuilder<RouletteCategoryChat> builder)
        {
            builder
                .HasKey(i => new { i.ChatId, i.RouletteCategoryId });

            builder
                .HasOne(i => i.Chat)
                .WithMany(i => i.WinnerCategoryChats)
                .HasForeignKey(i => i.ChatId);

            builder
                .HasOne(i => i.RouletteCategory)
                .WithMany(i => i.RouletteCategoryChats)
                .HasForeignKey(i => i.RouletteCategoryId);
        }
    }
}