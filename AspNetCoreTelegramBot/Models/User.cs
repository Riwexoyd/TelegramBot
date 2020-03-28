using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AspNetCoreTelegramBot.Models
{
    public class User : IEquatable<User>
    {
        public int Id { get; set; }

        public int TelegramId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public DateTime RegisterDate { get; set; }

        public string Login { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as User);
        }

        public bool Equals([AllowNull] User other)
        {
            return other != null &&
                   Id == other.Id &&
                   TelegramId == other.TelegramId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, TelegramId);
        }

        public static bool operator ==(User left, User right)
        {
            return EqualityComparer<User>.Default.Equals(left, right);
        }

        public static bool operator !=(User left, User right)
        {
            return !(left == right);
        }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(i => i.Id);

            builder
                .HasIndex(i => i.TelegramId)
                .IsUnique(true);

            builder
                .Property(i => i.RegisterDate)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("now()");

            builder
                .HasIndex(i => i.Login)
                .IsUnique(true);
        }
    }
}