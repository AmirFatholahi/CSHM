using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CSHM.Domain;

namespace CSHM.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(name: "IDNUsers");
        //builder.HasKey(x => x.ID);
        builder.Property(x => x.Id).HasColumnName("ID");
        builder.Property(x => x.NID).IsRequired().HasMaxLength(30);
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(30);
        builder.Property(x => x.FullName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.AliasName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.FatherName).IsRequired().HasMaxLength(30);
        builder.Property(x => x.RegDate).IsRequired().HasMaxLength(10);
        builder.Property(x => x.RegNumber).IsRequired().HasMaxLength(20);
        builder.Property(x => x.UserName).IsRequired().HasMaxLength(250);
        builder.Property(x => x.PostalCode).HasMaxLength(13);
        builder.Property(x => x.Address).HasMaxLength(500);
        builder.Property(x => x.Avatar).IsRequired(false);
        builder.Property(x => x.SecretKey).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Website).IsRequired(false).HasMaxLength(250);
        builder.Property(x => x.Cellphone).IsRequired(false).HasMaxLength(11);
        builder.Property(x => x.Phone).IsRequired(false).HasMaxLength(12);
        builder.Property(x => x.Email).IsRequired(false).HasMaxLength(250);
        builder.Property(x => x.NormalizedUserName).HasMaxLength(250);
        builder.Property(x => x.PasswordHash).HasMaxLength(500);
        builder.Property(x => x.SecurityStamp).HasMaxLength(500);
        builder.Property(x => x.ConcurrencyStamp).HasMaxLength(500);
        builder.Property(x => x.RegistrationDate).IsRequired(false).HasMaxLength(10);
        builder.Property(x => x.IsForced).IsRequired();
        builder.Property(x => x.LockoutEnabled).IsRequired();
        builder.Property(x => x.AccessFailedCount).IsRequired();

        builder.Property(x => x.ExporterID).HasDefaultValue(0);

        //builder.Ignore(e => e.Email);
        builder.Ignore(e => e.NormalizedEmail);
        //builder.Ignore(e => e.EmailConfirmed);
        builder.Ignore(e => e.PhoneNumber);
        builder.Ignore(e => e.PhoneNumberConfirmed);
        builder.Ignore(e => e.TwoFactorEnabled);

        builder.Property(x => x.Longitude).IsRequired(false).HasMaxLength(50);
        builder.Property(x => x.Latitude).IsRequired(false).HasMaxLength(50);



        builder.HasOne(x => x.Exporter)
         .WithMany()
         .HasForeignKey(x => x.ExporterID)
         .IsRequired()
         .OnDelete(DeleteBehavior.Restrict);


        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.CreatorID).IsRequired();
        builder.Property(x => x.CreationDateTime).IsRequired().HasColumnType("DateTime");
        builder.Property(x => x.ModificationDateTime).HasColumnType("DateTime");

    }
}
