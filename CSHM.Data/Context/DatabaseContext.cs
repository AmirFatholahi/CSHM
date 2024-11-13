using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using CSHM.Data.Configurations;
using CSHM.Domain;
using CSHM.Domain.Interfaces;
using CSHM.Domain.Models;

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

    // ================================================================================Blog

    public DbSet<Blog> Blogs { get; set; }

    public DbSet<BlogOccupation> BlogOccupations { get; set; }

    public DbSet<BlogPublisher> BlogPublishers { get; set; }

    public DbSet<BlogStatusType> BlogStatusTypes { get; set; }

    public DbSet<BlogType> BlogTypes { get; set; }

    // ================================================================================Product

    public DbSet<ProductCategoryType> ProductCategoryTypes { get; set; }

    public DbSet<ProductComment> ProductComments { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<ProductLable> ProductLables { get; set; }

    public DbSet<ProductOccupation> ProductOccupations { get; set; }

    public DbSet<ProductPublisher> ProductPublishers { get; set; }

    public DbSet<ProductType> ProductTypes { get; set; }

    // ================================================================================Publisher

    public DbSet<Publisher> Publishers { get; set; }

    public DbSet<PublisherBranch> PublisherBranches { get; set; }

    public DbSet<PublishType> PublishTypes { get; set; }

    // ================================================================================Other

    public DbSet<Lable> Lables { get; set; }

    public DbSet<Language> Languages { get; set; }

    public DbSet<Occupation> Occupations { get; set; }

    public DbSet<CategoryType> CategoryTypes { get; set; }

    public DbSet<CoverType> CoverTypes { get; set; }

    public DbSet<SizeType> SizeTypes { get; set; }

    // ================================================================================Person

    public DbSet<Person> People { get; set; }

    public DbSet<PersonOccupation> PersonOccupations { get; set; }

    // ================================================================================Media

    public DbSet<Media> Medias { get; set; }

    public DbSet<MediaType> MediaTypes { get; set; }
}