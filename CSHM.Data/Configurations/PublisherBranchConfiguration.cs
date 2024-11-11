using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CSHM.Domain;

namespace CSHM.Data.Configurations
{
    public class PublisherBranchConfiguration : IEntityTypeConfiguration<PublisherBranch>
    {
        public void Configure(EntityTypeBuilder<PublisherBranch> builder)
        {
            builder.ToTable(name: "PublisherBranches");

            builder.HasKey(x => x.ID);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Phone).IsRequired(false).HasMaxLength(11);
            builder.Property(x => x.Cellphone).IsRequired(false).HasMaxLength(11);
            builder.Property(x => x.Address).IsRequired(false).HasMaxLength(500);

            builder.HasOne(x => x.Publisher)
                   .WithMany()
                   .HasForeignKey(x => x.PublisherID)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.GeoCity)
                   .WithMany()
                   .HasForeignKey(x => x.GeoCityID)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.CreatorID).IsRequired();
            builder.Property(x => x.CreationDateTime).IsRequired().HasColumnType("DateTime");
            builder.Property(x => x.ModificationDateTime).HasColumnType("DateTime");

        }

    }
}
