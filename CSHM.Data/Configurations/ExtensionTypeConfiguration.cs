using CSHM.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSHM.Data.Configurations;

public class ExtensionTypeConfiguration : IEntityTypeConfiguration<ExtensionType>
{
    public void Configure(EntityTypeBuilder<ExtensionType> builder)
    {
        builder.ToTable(name: "ExtensionTypes");
        builder.HasKey(x => x.ID);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
        builder.Property(x => x.MatcherType).IsRequired(false).HasMaxLength(100);
        builder.Property(x => x.Matcher).IsRequired(false);

        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.CreatorID).IsRequired();
        builder.Property(x => x.CreationDateTime).IsRequired().HasColumnType("DateTime");
        builder.Property(x => x.ModificationDateTime).HasColumnType("DateTime");

    }
}
