using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CSHM.Domain;

namespace CSHM.Data.Configurations;

public class NotificationTypeConfiguration : IEntityTypeConfiguration<NotificationType>
{
    public void Configure(EntityTypeBuilder<NotificationType> builder)
    {
        builder.ToTable(name: "NotificationTypes");

        builder.HasKey(x => x.ID);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(50);

        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.CreatorID).IsRequired();
        builder.Property(x => x.CreationDateTime).IsRequired().HasColumnType("DateTime");
        builder.Property(x => x.ModificationDateTime).HasColumnType("DateTime");

    }
}
