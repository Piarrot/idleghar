using IdlegharDotnetDomain.Tests;
using NUnit.Framework;

namespace IdlegharDotnetDomain.UseCases.Players.Tests
{
    public class GetCurrentPlayerInventoryTests : BaseTests
    {
        [Test]
        public async Task ShouldReturnTheCurrentPlayersInventory()
        {
            var player = await FakePlayerFactory.CreateAndStorePlayerAndCharacter();
            GetCurrentPlayersInventory useCase = new(PlayersProvider);
            var result = await useCase.Handle(new AuthenticatedRequest(player));

            Assert.That(result.Id, Is.EqualTo(player.Id));
            Assert.That(result.Email, Is.EqualTo(player.Email));
            Assert.That(result.Username, Is.EqualTo(player.Username));
            Assert.That(result.Currency, Is.EqualTo(player.Currency));
            Assert.That(result.Items.Count, Is.EqualTo(player.Items.Count));
        }
    }
}