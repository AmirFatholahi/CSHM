using CSHM.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace CSHM.Data.Configurations
{
    public class ProductGenreTypeConfiguration : IEntityTypeConfiguration<ProductGenreType>
    {
        public void Configure(EntityTypeBuilder<ProductGenreType> builder)
        {
            builder.ToTable(name: "ProductGenreTypes");

            builder.HasKey(x => x.ID);

            builder.HasOne(x => x.Product)
                   .WithMany()
                   .HasForeignKey(x => x.ProductID)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.GenreType)
                   .WithMany()
                   .HasForeignKey(x => x.GenreTypeID)
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
