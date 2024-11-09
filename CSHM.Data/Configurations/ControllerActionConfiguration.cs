using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CSHM.Domain;

namespace CSHM.Data.Configurations;

public class ControllerActionConfiguration : IEntityTypeConfiguration<ControllerAction>
{
    public void Configure(EntityTypeBuilder<ControllerAction> builder)
    {
        builder.ToTable(name: "IDNControllerActions");

        builder.HasKey(x => x.ID);
        builder.Property(x => x.Code).IsRequired();
        builder.Property(x => x.TitleFa).IsRequired().HasMaxLength(50);
        builder.Property(x => x.TitleEn).IsRequired().HasMaxLength(50);
        builder.Property(x => x.ControllerName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.ActionName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Priority).IsRequired();

        builder.HasOne(x => x.Page)
           .WithMany(y => y.ControllerActions)
           .HasForeignKey(x => x.PageID)
           .IsRequired()
           .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.CreatorID).IsRequired();
        builder.Property(x => x.CreationDateTime).IsRequired().HasColumnType("DateTime");
        builder.Property(x => x.ModificationDateTime).HasColumnType("DateTime");

    }
}
