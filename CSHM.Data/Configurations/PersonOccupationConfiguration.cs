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
    public class PersonOccupationConfiguration : IEntityTypeConfiguration<PersonOccupation>
    {
        public void Configure(EntityTypeBuilder<PersonOccupation> builder)
        {
            builder.ToTable(name: "PersonOccupations");

            builder.HasKey(x => x.ID);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(150);


            builder.HasOne(x => x.Person)
                   .WithMany()
                   .HasForeignKey(x => x.PersonID)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Occupation)
                   .WithMany()
                   .HasForeignKey(x => x.OccupationID)
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
