using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CSHM.Domain;

namespace CSHM.Data.Configurations;

public class PageConfiguration : IEntityTypeConfiguration<Page>
{
    public void Configure(EntityTypeBuilder<Page> builder)
    {
        builder.ToTable(name: "IDNPages");

        builder.HasKey(x => x.ID);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Path).HasMaxLength(250);
        builder.Property(x => x.Icon).HasMaxLength(50);
        builder.Property(x => x.Priority).IsRequired();
        builder.Property(x => x.IsMenu).IsRequired();

        builder.HasOne(x => x.Parent)
           .WithMany(y => y.Children)
           .HasForeignKey(x => x.ParentID)
           .IsRequired(false)
           .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.CreatorID).IsRequired();
        builder.Property(x => x.CreationDateTime).IsRequired().HasColumnType("DateTime");
        builder.Property(x => x.ModificationDateTime).HasColumnType("DateTime");

    }
}
