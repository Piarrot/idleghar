using IdlegharDotnetDomain.Tests;
using IdlegharDotnetShared.Constants;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Factories.Tests
{
    public class RewardFactoryTests : BaseTests
    {
        [Test]
        public void QuestRewardsShouldConformWithSpecs()
        {
            RewardFactory rf = new(RandomnessProvider);

            foreach (Difficulty questDifficulty in Enum.GetValues(typeof(Difficulty)))
            {
                var rewards = rf.CreateQuestRewards(questDifficulty);

                // Assert.That()
            }

        }
    }
}