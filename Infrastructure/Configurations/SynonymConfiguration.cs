using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Configurations
{
    internal class SynonymConfiguration : IEntityTypeConfiguration<Synonym>
    {
        public void Configure(EntityTypeBuilder<Synonym> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                // Rule added based on the longest word in the english dictrionary (x2) https://en.wikipedia.org/wiki/Longest_word_in_English
                .HasMaxLength(90);

            builder.Property(s => s.NormalizedName)
                .IsRequired()
                // Rule added based on the longest word in the english dictrionary (x2) https://en.wikipedia.org/wiki/Longest_word_in_English
                .HasMaxLength(90);

            builder                
                .HasIndex(b => b.NormalizedName)
                .IsUnique()
                .HasDatabaseName("IX_Synonym_NormalizedName");

            builder.Property(s => s.Description)
                .IsRequired()
                .HasMaxLength(300);
        }
    }
}
