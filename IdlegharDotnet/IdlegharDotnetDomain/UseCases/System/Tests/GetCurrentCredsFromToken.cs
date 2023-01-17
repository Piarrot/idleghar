using IdlegharDotnetDomain.Tests;
using IdlegharDotnetDomain.UseCases.Players;
using NUnit.Framework;

namespace IdlegharDotnetDomain.UseCases.System.Tests
{
    public class GetCurrentPlayerCredsFromTokenTests : BaseTests
    {
        [Test]
        public async Task GivenAValidPlayerItShouldReturnThePlayerSuccessfully()
        {
            var player = await FakePlayerFactory.CreateAndStorePlayer();
            var token = AuthProvider.GenerateToken(player);
            var useCase = new GetCurrentPlayerCredsFromToken(PlayersProvider, AuthProvider);
            var playerCreds = await useCase.Handle(token);

            Assert.That(playerCreds.Id, Is.EqualTo(player.Id));
            Assert.That(playerCreds.EmailValidated, Is.EqualTo(player.EmailValidated));
            Assert.That(playerCreds.Email, Is.EqualTo(player.Email));
            Assert.That(playerCreds.Password, Is.EqualTo(player.Password));
        }
    }
}