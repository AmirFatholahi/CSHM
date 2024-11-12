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
    public class ProductCommentConfiguration : IEntityTypeConfiguration<ProductComment>
    {
        public void Configure(EntityTypeBuilder<ProductComment> builder)
        {
            builder.ToTable(name: "ProductComments");

            builder.HasKey(x => x.ID);
            builder.Property(x => x.Note).IsRequired().HasMaxLength(4000);
            builder.Property(x => x.NoteDate).IsRequired(false).HasMaxLength(10);
            builder.Property(x => x.NoteTime).IsRequired(false).HasMaxLength(10);


            builder.HasOne(x => x.Product)
                   .WithMany()
                   .HasForeignKey(x => x.ProductID)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.User)
                   .WithMany()
                   .HasForeignKey(x => x.UserID)
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
