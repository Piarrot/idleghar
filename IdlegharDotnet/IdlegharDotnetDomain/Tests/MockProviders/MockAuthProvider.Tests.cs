using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.Tests.MockProviders
{
    public class MockAuthProvider : IAuthProvider
    {
        public string GenerateToken(User user)
        {
            return $"[TOKEN]{user.Email}[TOKEN]";
        }

        public string? ParseTokenEmail(string token)
        {
            return token.Replace("[TOKEN]", "");
        }
    }
}