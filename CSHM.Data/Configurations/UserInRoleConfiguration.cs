using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CSHM.Domain;

namespace CSHM.Data.Configurations;

public class UserInRoleConfiguration : IEntityTypeConfiguration<UserInRole>
{
    public void Configure(EntityTypeBuilder<UserInRole> builder)
    {
        builder.ToTable(name: "IDNUsersInRoles");

        builder.HasKey(ur => new { ur.UserId, ur.RoleId });
        builder.Property(x => x.UserId).HasColumnName("UserID");
        builder.Property(x => x.RoleId).HasColumnName("RoleID");
        builder.Property(x => x.ExpiryDate).HasColumnType("DateTime");

        builder.HasOne(x => x.Role)
            .WithMany(y => y.UserInRoles)
            .HasForeignKey(x => x.RoleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.User)
            .WithMany(y => y.UserInRoles)
            .HasForeignKey(x => x.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

       

        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.CreatorID).IsRequired();
        builder.Property(x => x.CreationDateTime).IsRequired().HasColumnType("DateTime");
        builder.Property(x => x.ModificationDateTime).HasColumnType("DateTime");

    }
}
