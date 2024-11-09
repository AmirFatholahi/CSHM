using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CSHM.Data.Context;

public class AuthorizationDbContext<TUser, TRole, TKey, TUserInRoles> : IdentityDbContext<TUser, TRole, TKey, IdentityUserClaim<TKey>, TUserInRoles, IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>>
    where TUser : IdentityUser<TKey>
    where TRole : IdentityRole<TKey>
    where TKey : IEquatable<TKey>
    where TUserInRoles : IdentityUserRole<TKey>
{
    private readonly IOptions<OperationalStoreOptions> _operationalStoreOptions;

    public AuthorizationDbContext(
        DbContextOptions options,
        IOptions<OperationalStoreOptions> operationalStoreOptions)
        : base(options)
    {
        _operationalStoreOptions = operationalStoreOptions;
    }
}