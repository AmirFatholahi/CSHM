using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CSHM.Domain;

namespace CSHM.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(name: "IDNRoles");
        //builder.HasKey(x => x.ID);
        builder.Property(x => x.Id).HasColumnName("ID");
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.NormalizedName).HasMaxLength(50);
        builder.Property(x => x.ConcurrencyStamp).HasMaxLength(500);
        builder.Property(x => x.Side).IsRequired(false).HasMaxLength(10);

        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.CreatorID).IsRequired();
        builder.Property(x => x.CreationDateTime).IsRequired().HasColumnType("DateTime");
        builder.Property(x => x.ModificationDateTime).HasColumnType("DateTime");

    }
}
