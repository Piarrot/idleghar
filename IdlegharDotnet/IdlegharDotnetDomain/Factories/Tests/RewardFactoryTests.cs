using IdlegharDotnetDomain.Entities.Rewards;
using IdlegharDotnetDomain.Tests;
using IdlegharDotnetShared.SharedConstants;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Factories.Tests
{
    public class RewardFactoryTests : BaseTests
    {
        [Test]
        public void GivenAQuestDifficultyItShouldCreateARewardForThatTypeOfQuest()
        {
            RewardFactory rf = new(RandomnessProviderMock.Object);
            RandomnessProviderMock.Setup((r) => r.GetRandomItemQualityQuestRewardFromDifficulty(Difficulty.NORMAL)).Returns(ItemQuality.Enchanted);

            var reward = rf.CreateQuestRewards(Difficulty.NORMAL);

            Assert.That(reward.XP, Is.EqualTo(20));
            Assert.That(reward.Items[0].Quality, Is.EqualTo(ItemQuality.Enchanted));
        }
    }
}