using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreTelegramBot.Models.Configurations
{
    public class UserChatConfiguration : IEntityTypeConfiguration<UserChat>
    {
        public void Configure(EntityTypeBuilder<UserChat> builder)
        {
            builder
                .HasKey(i => new { i.UserId, i.ChatId });

            builder
                .HasOne(i => i.User)
                .WithMany(i => i.UserChats)
                .HasForeignKey(i => i.UserId);

            builder
                .HasOne(i => i.Chat)
                .WithMany(i => i.UserChats)
                .HasForeignKey(i => i.ChatId);
        }
    }
}