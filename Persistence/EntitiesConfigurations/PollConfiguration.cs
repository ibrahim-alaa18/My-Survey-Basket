using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MySurveyBasket.Persistence.EntitiesConfigurations
{
    public class PollConfiguration : IEntityTypeConfiguration<Poll>
    {
        public void Configure(EntityTypeBuilder<Poll> builder)
        {
            builder.HasIndex(p => p.Title).IsUnique();
            builder.Property(p => p.Title).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Summary).HasMaxLength(1500);
        }
    }
}
