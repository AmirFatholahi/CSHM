using CSHM.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace CSHM.Data.Configurations
{
    public class BlogPublisherConfiguration : IEntityTypeConfiguration<BlogPublisher>
    {
        public void Configure(EntityTypeBuilder<BlogPublisher> builder)
        {
            builder.ToTable(name: "BlogPublishers");

            builder.HasKey(x => x.ID);


            builder.HasOne(x => x.Blog)
                   .WithMany()
                   .HasForeignKey(x => x.BlogID)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Publisher)
                   .WithMany()
                   .HasForeignKey(x => x.PublisherID)
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
