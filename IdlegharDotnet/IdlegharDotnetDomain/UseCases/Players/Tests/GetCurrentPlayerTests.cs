using IdlegharDotnetDomain.Tests;
using NUnit.Framework;

namespace IdlegharDotnetDomain.UseCases.Players.Tests
{
    public class GetCurrentPlayerTests : BaseTests
    {
        [Test]
        public async Task ShouldReturnTheCurrentPlayer()
        {
            var player = await FakePlayerFactory.CreateAndStorePlayerAndCharacter();
            GetCurrentPlayer useCase = new();
            var result = useCase.Handle(new AuthenticatedRequest(player));

            Assert.That(result.Id, Is.EqualTo(player.Id));
            Assert.That(result.Username, Is.EqualTo(player.Username));
            Assert.That(result.Email, Is.EqualTo(player.Email));
            Assert.That(result.Currency, Is.EqualTo(player.Currency));
        }
    }
}