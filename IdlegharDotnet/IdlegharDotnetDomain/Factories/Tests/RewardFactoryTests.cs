using System.Linq.Expressions;
using IdlegharDotnetDomain.Entities.Rewards;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetDomain.Tests;
using IdlegharDotnetShared.Constants;
using Moq;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Factories.Tests
{
    public class RewardFactoryTests : BaseTests
    {
        [Test]
        [TestCase(Difficulty.EASY, 0.11, ItemQuality.Exceptional)]
        public void QuestRewardsShouldConformWithSpecs(Difficulty difficulty, double randValue, ItemQuality quality)
        {
            RewardFactory rf = new(RandomnessProviderMock.Object);
            RandomnessProviderMock.Setup(MockRandomDoubleLambda).Returns(randValue);

            var rewards = rf.CreateQuestRewards(difficulty);
            var itemRewards = rewards.OfType<EquipmentReward>().ToList();

            Assert.That(itemRewards.Count, Is.EqualTo(1));
            Assert.That(itemRewards[0].Equipment.Quality, Is.EqualTo(quality));
        }
    }
}