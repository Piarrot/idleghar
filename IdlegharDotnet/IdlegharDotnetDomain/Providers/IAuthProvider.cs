using IdlegharDotnetDomain.Entities;

namespace IdlegharDotnetDomain.Providers
{
    public interface IAuthProvider
    {
        string GenerateToken(User user);
        string? ParseTokenEmail(string token);
    }
}