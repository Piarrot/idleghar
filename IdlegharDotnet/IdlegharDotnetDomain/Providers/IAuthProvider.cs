using IdlegharDotnetDomain.Entities;

namespace IdlegharDotnetDomain.Providers
{
    public interface IAuthProvider
    {
        string GenerateToken(Player player);
        string? ParseTokenEmail(string token);
    }
}