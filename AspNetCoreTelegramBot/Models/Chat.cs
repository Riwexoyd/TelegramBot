using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;

using Telegram.Bot.Types.Enums;

namespace AspNetCoreTelegramBot.Models
{
    public class Chat
    {
        public int Id { get; set; }

        public long TelegramId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ChatType TelegramChatType { get; set; }

        public DateTime RegisterDate { get; set; }
    }

    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasKey(i => i.Id);
            builder.HasIndex(i => i.TelegramId)
                .IsUnique(true);
            builder.Property(i => i.RegisterDate)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("now()");
        }
    }
}