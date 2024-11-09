using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using CSHM.Data.Configurations;
using CSHM.Domain;
using CSHM.Domain.Interfaces;

namespace CSHM.Data.Context;

public class DatabaseContext : AuthorizationDbContext<User, Role, int, UserInRole>
{
    public DatabaseContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //==================================== For Lazy Loading
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        // ====================================================================== Configuration

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PageConfiguration).Assembly);




        // ====================================================================== IDENTITY 4.0

        modelBuilder.Ignore<IdentityUserClaim<int>>();
        modelBuilder.Ignore<IdentityUserLogin<int>>();
        modelBuilder.Ignore<IdentityRoleClaim<int>>();
        modelBuilder.Ignore<IdentityUserToken<int>>();

        // ====================================================================== For Filtering of IsDeleted From Data in Lazy Loading

        foreach (var type in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(IEntity).IsAssignableFrom(type.ClrType))
                modelBuilder.SetSoftDeleteFilter(type.ClrType);
        }
    }



    // ============================================================================================================================================
    // ============================================================================================================================================
    // ============================================================================================================================================
    // ============================================================================================================================================
    // ============================================================================================================================================

    // ================================================================================IDENTITY

    public DbSet<Page> IDNPages { get; set; }

    public DbSet<RoleClaim> IDNRoleClaims { get; set; }

    public DbSet<ControllerAction> IDNControllerActions { get; set; }

    public DbSet<PolicyParameter> IDNPolicyParameters { get; set; }

    public DbSet<Policy> IDNPolicies { get; set; }

    public DbSet<UserPolicy> IDNUserPolicies { get; set; }

    public DbSet<UserType> IDNUserTypes{ get; set; }


}