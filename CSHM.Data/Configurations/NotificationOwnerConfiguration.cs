using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CSHM.Domain;

namespace CSHM.Data.Configurations;

public class NotificationOwnerConfiguration : IEntityTypeConfiguration<NotificationOwner>
{
    public void Configure(EntityTypeBuilder<NotificationOwner> builder)
    {
        builder.ToTable(name: "NotificationOwners");

        builder.HasKey(x => x.ID);
        builder.Property(x => x.SeenDateTime).HasMaxLength(21);

        builder.HasOne(x => x.Notification)
         .WithMany(y => y.NotificationOwners)
         .HasForeignKey(x => x.NotificationID)
         .IsRequired()
         .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.User)
         .WithMany()
         .HasForeignKey(x => x.UserID)
         .IsRequired()
         .OnDelete(DeleteBehavior.Restrict);


        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.CreatorID).IsRequired();
        builder.Property(x => x.CreationDateTime).IsRequired().HasColumnType("DateTime");
        builder.Property(x => x.ModificationDateTime).HasColumnType("DateTime");

    }
}
