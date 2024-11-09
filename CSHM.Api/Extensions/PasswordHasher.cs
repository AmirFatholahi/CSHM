using CSHM.Domain;
using Microsoft.AspNetCore.Identity;

namespace CSHM.Api.Extensions;

public class PasswordHasher : IPasswordHasher<User>
{
    public string HashPassword(User user, string password)
    {
        throw new NotImplementedException();
    }

    public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
    {
        throw new NotImplementedException();
    }
}