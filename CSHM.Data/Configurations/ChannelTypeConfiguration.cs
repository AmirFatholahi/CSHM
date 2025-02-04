using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CSHM.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Data.Configurations
{
    public class ChannelTypeConfiguration : IEntityTypeConfiguration<ChannelType>
    {
        public void Configure(EntityTypeBuilder<ChannelType> builder)
        {
            builder.ToTable(name: "ChannelTypes");
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
