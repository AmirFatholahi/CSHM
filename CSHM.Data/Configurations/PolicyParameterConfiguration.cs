using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CSHM.Domain;

namespace CSHM.Data.Configurations;

public class PolicyParameterConfiguration : IEntityTypeConfiguration<PolicyParameter>
{
    public void Configure(EntityTypeBuilder<PolicyParameter> builder)
    {
        builder.ToTable(name: "IDNPolicyParameters");

        builder.HasKey(x => x.ID);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Side).IsRequired(false).HasMaxLength(10);
        builder.Property(x => x.IsMultiple).IsRequired();

        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.CreatorID).IsRequired();
        builder.Property(x => x.CreationDateTime).IsRequired().HasColumnType("DateTime");
        builder.Property(x => x.ModificationDateTime).HasColumnType("DateTime");

    }
}
