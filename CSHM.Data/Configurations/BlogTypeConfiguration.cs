using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CSHM.Domain;


namespace CSHM.Data.Configurations
{
    public class BlogTypeConfiguration : IEntityTypeConfiguration<BlogType>
    {
        public void Configure(EntityTypeBuilder<BlogType> builder)
        {
            builder.ToTable(name: "BlogTypes");

            builder.HasKey(x => x.ID);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(250);
            builder.Property(x => x.TitleEN).IsRequired(false).HasMaxLength(250);





            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.CreatorID).IsRequired();
            builder.Property(x => x.CreationDateTime).IsRequired().HasColumnType("DateTime");
            builder.Property(x => x.ModificationDateTime).HasColumnType("DateTime");

        }
    }
}
