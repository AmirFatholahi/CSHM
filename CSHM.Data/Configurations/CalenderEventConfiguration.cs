using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CSHM.Domain;

namespace CSHM.Data.Configurations;

public class CalenderEventConfiguration : IEntityTypeConfiguration<CalenderEvent>
{
    public void Configure(EntityTypeBuilder<CalenderEvent> builder)
    {
        builder.ToTable(name: "CalenderEvents");

        builder.HasKey(x => x.ID);
        builder.Property(x => x.Month).IsRequired();
        builder.Property(x => x.Day).IsRequired();
        builder.Property(x => x.CalenderType).HasMaxLength(20);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(500);
        builder.Property(x => x.IsHoliday).IsRequired();

        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.CreatorID).IsRequired();
        builder.Property(x => x.CreationDateTime).IsRequired().HasColumnType("DateTime");
        builder.Property(x => x.ModificationDateTime).HasColumnType("DateTime");

    }
}
