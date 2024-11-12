using CSHM.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSHM.Domain.Models;

namespace CSHM.Data.Configurations
{
    public class ProductPublisherConfiguration : IEntityTypeConfiguration<ProductPublisher>
    {
        public void Configure(EntityTypeBuilder<ProductPublisher> builder)
        {
            builder.ToTable(name: "ProductPublishers");

            builder.HasKey(x => x.ID);


            builder.HasOne(x => x.Product)
                   .WithMany()
                   .HasForeignKey(x => x.ProductID)
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
