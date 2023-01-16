using IdlegharDotnetDomain.Entities;
using NUnit.Framework;

namespace IdlegharDotnetBackend.Providers.Tests
{
    public class JWTProviderTests
    {
        [Test]
        public void GivenAUserShouldCreateAValidToken()
        {
            var email = "cool_guy_69@email.com";
            var provider = new JWTProvider("los gatitos son lo mejor");
            var token = provider.GenerateToken(new Player { Email = email, Username = "CoolGuy69" });
            Assert.That(token, Is.Not.Null);
            var parsedEmail = provider.ParseTokenEmail(token);
            Assert.That(parsedEmail, Is.EqualTo(email));
        }

    }
}