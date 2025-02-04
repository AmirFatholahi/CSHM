using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CSHM.Domain;

namespace CSHM.Data.Configurations;

public class NotificationDraftConfiguration : IEntityTypeConfiguration<NotificationDraft>
{
    public void Configure(EntityTypeBuilder<NotificationDraft> builder)
    {
        builder.ToTable(name: "NotificationDrafts");

        builder.HasKey(x => x.ID);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(250);
        builder.Property(x => x.Subject).IsRequired().HasMaxLength(250);
        builder.Property(x => x.MessageBody).IsRequired();
        builder.Property(x => x.Status).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Path).HasMaxLength(500);

        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.CreatorID).IsRequired();
        builder.Property(x => x.CreationDateTime).IsRequired().HasColumnType("DateTime");
        builder.Property(x => x.ModificationDateTime).HasColumnType("DateTime");

    }
}
