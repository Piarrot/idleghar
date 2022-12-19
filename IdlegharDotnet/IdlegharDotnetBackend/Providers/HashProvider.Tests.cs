namespace IdlegharDotnetBackendTests;

using System.Text.RegularExpressions;
using IdlegharDotnetBackend;
using NUnit.Framework;

public class HashProviderTests
{
    CryptoProvider cryptoProvider = new CryptoProvider();

    [Test]
    public void GivenAPlainPasswordWeCanHashItAndCompareItToItsHashedVersion()
    {
        var plainPassword = "user1234";
        var otherPlainPassword = "banana4321";

        var hashedPassword1 = cryptoProvider.HashPassword(plainPassword);
        var hashedPassword2 = cryptoProvider.HashPassword(plainPassword);
        var otherHashedPassword = cryptoProvider.HashPassword(otherPlainPassword);

        //Hashed passwords do not collide, even for the same password
        Assert.AreNotEqual(hashedPassword1, otherHashedPassword);
        Assert.AreNotEqual(hashedPassword1, hashedPassword2);

        //Any hash of a password matches the plain password
        Assert.IsTrue(cryptoProvider.DoesPasswordMatches(hashedPassword1, plainPassword));
        Assert.IsTrue(cryptoProvider.DoesPasswordMatches(hashedPassword2, plainPassword));

        //A hash of another password do not match the password
        Assert.IsFalse(cryptoProvider.DoesPasswordMatches(hashedPassword1, otherPlainPassword));
    }

    [Test]
    public void GivenANumberOfDigitsCanGenerateAStringOfRandomNumbers()
    {
        var randomNumbers1 = cryptoProvider.GetRandomNumberDigits(6);
        var randomNumbers2 = cryptoProvider.GetRandomNumberDigits(6);
        Assert.IsTrue(Regex.IsMatch(randomNumbers1, "^\\d{6}$"));
        Assert.AreNotEqual(randomNumbers1, randomNumbers2);
    }
}