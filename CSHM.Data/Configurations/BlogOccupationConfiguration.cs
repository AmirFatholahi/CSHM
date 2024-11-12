using CSHM.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace CSHM.Data.Configurations
{
    public class BlogOccupationConfiguration : IEntityTypeConfiguration<BlogOccupation>
    {
        public void Configure(EntityTypeBuilder<BlogOccupation> builder)
        {
            builder.ToTable(name: "BlogOccupations");

            builder.HasKey(x => x.ID);


            builder.HasOne(x => x.Blog)
                   .WithMany()
                   .HasForeignKey(x => x.BlogID)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.PersonOccupation)
                   .WithMany()
                   .HasForeignKey(x => x.PersonOccupationID)
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
