using CSHM.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(name: "Products");

            builder.HasKey(x => x.ID);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(300);
            builder.Property(x => x.ISBN).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.PublishDate).IsRequired(false).HasMaxLength(10);
            builder.Property(x => x.MetaDescription).IsRequired(false).HasMaxLength(4000);
            builder.Property(x => x.Summary).IsRequired(false).HasMaxLength(2000);
            builder.Property(x => x.ProductCode).IsRequired().HasMaxLength(10);
            builder.Property(x => x.Barcode).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.StudyTime).HasPrecision(18,2);
            builder.Property(x => x.Rate).HasPrecision(18, 2);



            builder.HasOne(x => x.Publisher)
                   .WithMany()
                   .HasForeignKey(x => x.PublisherID)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ProductType)
                   .WithMany()
                   .HasForeignKey(x => x.ProductTypeID)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.PublishType)
                   .WithMany()
                   .HasForeignKey(x => x.PublishTypeID)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Language)
                   .WithMany()
                   .HasForeignKey(x => x.LanguageID)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.SizeType)
                   .WithMany()
                   .HasForeignKey(x => x.SizeTypeID)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CoverType)
                   .WithMany()
                   .HasForeignKey(x => x.CoverTypeID)
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
