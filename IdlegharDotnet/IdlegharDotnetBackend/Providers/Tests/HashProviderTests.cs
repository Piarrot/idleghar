using System.Text.RegularExpressions;
using NUnit.Framework;

namespace IdlegharDotnetBackend.Providers.Tests
{
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
            Assert.That(otherHashedPassword, Is.Not.EqualTo(hashedPassword1));
            Assert.That(hashedPassword2, Is.Not.EqualTo(hashedPassword1));

            //Any hash of a password matches the plain password
            Assert.That(cryptoProvider.DoesPasswordMatches(hashedPassword1, plainPassword), Is.True);
            Assert.That(cryptoProvider.DoesPasswordMatches(hashedPassword2, plainPassword), Is.True);

            //A hash of another password do not match the password
            Assert.That(cryptoProvider.DoesPasswordMatches(hashedPassword1, otherPlainPassword), Is.False);
        }

        [Test]
        public void GivenANumberOfDigitsCanGenerateAStringOfRandomNumbers()
        {
            var randomNumbers1 = cryptoProvider.GetRandomNumberDigits(6);
            var randomNumbers2 = cryptoProvider.GetRandomNumberDigits(6);
            Assert.That(Regex.IsMatch(randomNumbers1, "^\\d{6}$"), Is.True);
            Assert.That(randomNumbers2, Is.Not.EqualTo(randomNumbers1));
        }
    }
}