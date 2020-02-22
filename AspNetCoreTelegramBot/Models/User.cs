using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;

namespace AspNetCoreTelegramBot.Models
{
    public class User
    {
        public int Id { get; set; }

        public int TelegramId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public DateTime RegisterDate { get; set; }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
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