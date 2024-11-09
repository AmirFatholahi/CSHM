using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CSHM.Domain;

namespace CSHM.Data.Configurations;

public class UserPolicyConfiguration : IEntityTypeConfiguration<UserPolicy>
{
    public void Configure(EntityTypeBuilder<UserPolicy> builder)
    {
        builder.ToTable(name: "IDNUserPolicies");

        builder.HasKey(x => x.ID);

        builder.HasOne(x => x.User)
            .WithMany(y => y.UserPolicies)
            .HasForeignKey(x => x.UserID)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Policy)
             .WithMany()
             .HasForeignKey(x => x.PolicyID)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

       

        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.CreatorID).IsRequired();
        builder.Property(x => x.CreationDateTime).IsRequired().HasColumnType("DateTime");
        builder.Property(x => x.ModificationDateTime).HasColumnType("DateTime");
    }
}
