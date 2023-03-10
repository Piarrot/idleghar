using IdlegharDotnetDomain.Factories;
using IdlegharDotnetDomain.Tests;
using IdlegharDotnetShared.Players;
using IdlegharDotnetShared.SharedConstants;
using NUnit.Framework;

namespace IdlegharDotnetDomain.UseCases.Players.Tests
{
    public class ClaimRewardUseCaseTests : BaseTests
    {
        [Test]
        public async Task ClaimingAnItemRewardShouldAddTheItemToTheInventory()
        {
            RandomnessProviderMock.Setup((r) => r.GetRandomItemQualityQuestRewardFromDifficulty(Difficulty.NORMAL)).Returns(ItemQuality.Common);
            RewardFactory rf = new RewardFactory(RandomnessProviderMock.Object);
            var reward = rf.CreateQuestRewards(Difficulty.NORMAL);
            var player = await FakePlayerFactory.CreateAndStorePlayerAndCharacter();
            player.UnclaimedRewards.Add(reward);
            await StorageProvider.SavePlayer(player);

            var useCase = new ClaimRewardUseCase(StorageProvider);
            await useCase.Handle(new AuthenticatedRequest<ClaimRewardUseCaseRequest>(player, new(reward.Id)));

            var updatedPlayer = await StorageProvider.GetPlayerByIdOrThrow(player.Id);

            Assert.That(updatedPlayer.Items, Does.Contain(reward.Items[0]));
            Assert.That(updatedPlayer.UnclaimedRewards, Does.Not.Contain(reward));
            Assert.That(updatedPlayer.Character!.XP, Is.EqualTo(reward.XP));
        }

        [Test]
        public async Task ClaimingAnInvalidRewardShouldThrow()
        {
            RewardFactory rf = new RewardFactory(RandomnessProviderMock.Object);
            var reward = rf.CreateEquipmentReward(ItemQuality.Enchanted);
            var player = await FakePlayerFactory.CreateAndStorePlayerAndCharacter();

            var useCase = new ClaimRewardUseCase(StorageProvider);

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest<ClaimRewardUseCaseRequest>(player, new(reward.Id)));
            });

            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.INVALID_REWARD));
        }

        [Test]
        public async Task GivenAPlayerWithoutACharacterShouldFail()
        {
            RandomnessProviderMock.Setup((r) => r.GetRandomItemQualityQuestRewardFromDifficulty(Difficulty.NORMAL)).Returns(ItemQuality.Common);
            RewardFactory rf = new RewardFactory(RandomnessProviderMock.Object);
            var reward = rf.CreateQuestRewards(Difficulty.NORMAL);
            var player = await FakePlayerFactory.CreateAndStorePlayer();
            await StorageProvider.SavePlayer(player);

            var useCase = new ClaimRewardUseCase(StorageProvider);
            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest<ClaimRewardUseCaseRequest>(player, new(reward.Id)));
            });

            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.INVALID_PLAYER));
        }

        [Test]
        public async Task GivenEnoughXPACharacterShouldLevelUp()
        {
            RewardFactory rf = new RewardFactory(RandomnessProviderMock.Object);
            var player = await FakePlayerFactory.CreateAndStorePlayerAndCharacter();
            var initialLevel = player.Character!.Level;
            var reward = rf.CreateQuestRewards(Difficulty.NORMAL);
            reward.AddXP(1000);
            player.UnclaimedRewards.Add(reward);
            await StorageProvider.SavePlayer(player);

            var useCase = new ClaimRewardUseCase(StorageProvider);
            await useCase.Handle(new AuthenticatedRequest<ClaimRewardUseCaseRequest>(player, new(reward.Id)));

            var character = await StorageProvider.GetCharacterByPlayerIdOrThrow(player.Id);
            var newLevel = character.Level;
            Assert.That(initialLevel, Is.LessThan(newLevel));
        }
    }
}