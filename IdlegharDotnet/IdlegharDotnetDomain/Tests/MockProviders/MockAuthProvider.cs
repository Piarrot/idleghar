using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.Tests.MockProviders
{
    public class MockAuthProvider : IAuthProvider
    {
        public string GenerateToken(Player player)
        {
            return $"[TOKEN]{player.Email}[TOKEN]";
        }

        public string? ParseTokenEmail(string token)
        {
            return token.Replace("[TOKEN]", "");
        }
    }
}