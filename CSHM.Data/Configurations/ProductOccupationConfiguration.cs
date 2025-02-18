using CSHM.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace CSHM.Data.Configurations
{
    public class ProductOccupationConfiguration : IEntityTypeConfiguration<ProductOccupation>
    {
        public void Configure(EntityTypeBuilder<ProductOccupation> builder)
        {
            builder.ToTable(name: "ProductOccupations");

            builder.HasKey(x => x.ID);


            builder.HasOne(x => x.Product)
                   .WithMany()
                   .HasForeignKey(x => x.ProductID)
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
