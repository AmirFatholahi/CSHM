using CSHM.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSHM.Data.Configurations;

public class MediaConfiguration : IEntityTypeConfiguration<Media>
{
    public void Configure(EntityTypeBuilder<Media> builder)
    {

        builder.ToTable(name: "Medias");
        builder.HasKey(x => x.ID);
        builder.Property(x => x.FileName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.EntityName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.DisplayName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.ExtensionName).IsRequired().HasMaxLength(30);
        builder.Property(x => x.IsConfirm).IsRequired();
        builder.Property(x => x.IsDefault).IsRequired();

        builder.HasOne(x => x.MediaType)
            .WithMany()
            .HasForeignKey(x => x.MediaTypeID)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ExtensionType)
           .WithMany()
           .HasForeignKey(x => x.ExtensionTypeID)
           .IsRequired()
           .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Blog)
           .WithMany()
           .HasForeignKey(x => x.BlogID)
           .IsRequired()
           .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Product)
           .WithMany()
           .HasForeignKey(x => x.ProductID)
           .IsRequired()
           .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.CreatorID).IsRequired();
        builder.Property(x => x.CreationDateTime).IsRequired().HasColumnType("DateTime");
        builder.Property(x => x.ModificationDateTime).HasColumnType("DateTime");

    }
}
