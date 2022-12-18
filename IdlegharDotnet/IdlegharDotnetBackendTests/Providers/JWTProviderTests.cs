namespace IdlegharDotnetBackendTests;

using IdlegharDotnetBackend;

public class JWTProviderTests
{
    [Test]
    public void GivenAUserShouldCreateAValidToken()
    {
        var email = "cool_guy_69@email.com";
        var provider = new JWTProvider("los gatitos son lo mejor");
        var token = provider.GenerateToken(new User { Email = email, Username = "CoolGuy69" });
        Assert.IsNotNull(token);
        var parsedEmail = provider.ParseTokenEmail(token);
        Assert.AreEqual(email, parsedEmail);
    }

}