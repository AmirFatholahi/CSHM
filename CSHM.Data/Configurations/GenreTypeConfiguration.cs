using CSHM.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CSHM.Data.Configurations
{
    public class GenreTypeConfiguration : IEntityTypeConfiguration<GenreType>
    {
        public void Configure(EntityTypeBuilder<GenreType> builder)
        {
            builder.ToTable(name: "GenreTypes");

            builder.HasKey(x => x.ID);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(200);



            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.CreatorID).IsRequired();
            builder.Property(x => x.CreationDateTime).IsRequired().HasColumnType("DateTime");
            builder.Property(x => x.ModificationDateTime).HasColumnType("DateTime");

        }
    }
}
