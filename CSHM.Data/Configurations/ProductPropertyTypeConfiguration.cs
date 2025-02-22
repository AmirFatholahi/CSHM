using CSHM.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CSHM.Data.Configurations
{
    public class ProductPropertyTypeConfiguration : IEntityTypeConfiguration<ProductPropertyType>
    {
        public void Configure(EntityTypeBuilder<ProductPropertyType> builder)
        {
            builder.ToTable(name: "ProductPropertyTypes");

            builder.HasKey(x => x.ID);
            builder.Property(x => x.Value).IsRequired().HasMaxLength(200);



            builder.HasOne(x => x.Product)
                   .WithMany()
                   .HasForeignKey(x => x.ProductID)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.PropertyType)
                   .WithMany()
                   .HasForeignKey(x => x.PropertyTypeID)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);
;

            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.CreatorID).IsRequired();
            builder.Property(x => x.CreationDateTime).IsRequired().HasColumnType("DateTime");
            builder.Property(x => x.ModificationDateTime).HasColumnType("DateTime");

        }
    }
}
