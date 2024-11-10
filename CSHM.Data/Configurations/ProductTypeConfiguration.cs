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
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.ToTable(name: "ProductTypes");

            builder.HasKey(x => x.ID);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);






            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.CreatorID).IsRequired();
            builder.Property(x => x.CreationDateTime).IsRequired().HasColumnType("DateTime");
            builder.Property(x => x.ModificationDateTime).HasColumnType("DateTime");

        }
    }
}
