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
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ToTable(name: "Blogs");

            builder.HasKey(x => x.ID);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(300);
            builder.Property(x => x.Summary).IsRequired(false).HasMaxLength(1000);
            builder.Property(x => x.Content).IsRequired(false).HasMaxLength(2000);
            builder.Property(x => x.MetaDescription).IsRequired(false).HasMaxLength(4000);
            builder.Property(x => x.CreationDate).IsRequired(false).HasMaxLength(10);
            builder.Property(x => x.CreationTime).IsRequired(false).HasMaxLength(10);


            builder.HasOne(x => x.BlogType)
                   .WithMany()
                   .HasForeignKey(x => x.BlogTypeID)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.BlogStatusType)
                   .WithMany()
                   .HasForeignKey(x => x.BlogStatusTypeID)
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
