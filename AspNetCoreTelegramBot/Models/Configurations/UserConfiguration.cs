using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreTelegramBot.Models.Configurations
{
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
                .HasDefaultValueSql("now() at time zone 'utc'");

            builder
                .HasIndex(i => i.Login)
                .IsUnique(true);
        }
    }
}