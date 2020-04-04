using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreTelegramBot.Models.Configurations
{
    public class QuoteKeywordConfiguration : IEntityTypeConfiguration<QuoteKeyword>
    {
        public void Configure(EntityTypeBuilder<QuoteKeyword> builder)
        {
            builder
                .HasKey(i => new { i.QuoteId, i.KeywordId });

            builder
                .HasOne(i => i.Quote)
                .WithMany(i => i.QuoteKeywords)
                .HasForeignKey(i => i.QuoteId);

            builder
                .HasOne(i => i.Keyword)
                .WithMany(i => i.QuoteKeywords)
                .HasForeignKey(i => i.KeywordId);
        }
    }
}