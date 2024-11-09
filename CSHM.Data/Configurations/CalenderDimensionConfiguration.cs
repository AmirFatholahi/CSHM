using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CSHM.Domain;

namespace CSHM.Data.Configurations;

public class CalenderDimensionConfiguration : IEntityTypeConfiguration<CalenderDimension>
{
    public void Configure(EntityTypeBuilder<CalenderDimension> builder)
    {
        builder.ToTable(name: "CalenderDimensions");

        builder.HasKey(x => x.ID);
        builder.Property(x => x.Date).IsRequired().HasColumnType("Date");
        builder.Property(x => x.GregDate).IsRequired().HasMaxLength(20);
        builder.Property(x => x.GregKey).IsRequired();
        builder.Property(x => x.JalaliDate).IsRequired().HasMaxLength(20);
        builder.Property(x => x.HijriDate).IsRequired().HasMaxLength(20);
        builder.Property(x => x.HijriKey).IsRequired();
        builder.Property(x => x.YearKey).IsRequired();
        builder.Property(x => x.YearName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.SeasonKey).IsRequired();
        builder.Property(x => x.SeasonName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.MonthKey).IsRequired();
        builder.Property(x => x.MonthName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.DayOfWeekName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.DayName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.JalaliAlphabetic).IsRequired().HasMaxLength(250);
        builder.Property(x => x.GregSeasonName).IsRequired().HasMaxLength(250);
        builder.Property(x => x.GregSeasonNameFa).IsRequired().HasMaxLength(250);
        builder.Property(x => x.GregMonthName).IsRequired().HasMaxLength(250);
        builder.Property(x => x.GregMonthNameFa).IsRequired().HasMaxLength(250);
        builder.Property(x => x.Identifier).IsRequired().HasMaxLength(500);
        builder.Property(x => x.Hash).IsRequired().HasMaxLength(500);

        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.CreatorID).IsRequired();
        builder.Property(x => x.CreationDateTime).IsRequired().HasColumnType("DateTime");
        builder.Property(x => x.ModificationDateTime).HasColumnType("DateTime");

    }
}
