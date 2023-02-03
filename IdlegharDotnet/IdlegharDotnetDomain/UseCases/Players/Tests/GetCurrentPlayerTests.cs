using IdlegharDotnetDomain.Entities.Items;
using IdlegharDotnetDomain.Entities.Rewards;
using IdlegharDotnetDomain.Tests;
using NUnit.Framework;

namespace IdlegharDotnetDomain.UseCases.Players.Tests
{
    public class GetCurrentPlayerTests : BaseTests
    {
        [Test]
        public async Task ShouldReturnTheCurrentPlayers()
        {
            var player = await FakePlayerFactory.CreateAndStorePlayerAndCharacter();
            player.Currency = 15000;
            player.Items.Add(new Equipment
            {
                Name = "Cool Sword",
                Description = "A really cool sword to slice things",
                StatChanges = new()
                {
                    [Constants.Characters.Stat.DAMAGE] = 5
                }
            });
            player.UnclaimedRewards.Add(new XPReward());
            await PlayersProvider.Save(player);

            GetCurrentPlayer useCase = new(PlayersProvider);
            var result = await useCase.Handle(new AuthenticatedRequest(player));

            Assert.That(result.Id, Is.EqualTo(player.Id));
            Assert.That(result.Email, Is.EqualTo(player.Email));
            Assert.That(result.Username, Is.EqualTo(player.Username));
            Assert.That(result.Currency, Is.EqualTo(player.Currency));
            Assert.That(result.Items.Count, Is.EqualTo(player.Items.Count));
            Assert.That(result.UnclaimedRewards.Count, Is.EqualTo(player.UnclaimedRewards.Count));
            Assert.That(result.Character!.Id, Is.EqualTo(player.Character!.Id));
            Assert.That(result.Character.Level, Is.EqualTo(player.Character.Level));
        }
    }
}