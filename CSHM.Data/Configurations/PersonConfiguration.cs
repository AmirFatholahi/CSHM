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
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable(name: "People");

            builder.HasKey(x => x.ID);
            builder.Property(x => x.FirstName).IsRequired(false).HasMaxLength(70);
            builder.Property(x => x.FullName).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.AliasName).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.BirthDate).IsRequired(false).HasMaxLength(10);
            builder.Property(x => x.Biography).IsRequired(false).HasMaxLength(1000);

            builder.HasOne(x => x.GenderType)
                   .WithMany()
                   .HasForeignKey(x => x.GenderTypeID)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.GeoCountry)
                   .WithMany()
                   .HasForeignKey(x => x.GeoCountryID)
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
