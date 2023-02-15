using IdlegharDotnetDomain.Tests;
using IdlegharDotnetShared.Constants;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Factories.Tests
{
    public class QuestFactoryTests : BaseTests
    {
        [Test]
        [TestCase(Difficulty.EASY)]
        [TestCase(Difficulty.NORMAL)]
        [TestCase(Difficulty.HARD)]
        [TestCase(Difficulty.LEGENDARY)]
        public void CreatedQuestsShouldHaveEncounters(Difficulty difficulty)
        {
            var factory = new QuestFactory(RandomnessProviderMock.Object, TimeProvider);
            var quest = factory.CreateQuest(Guid.NewGuid().ToString(), difficulty);
            Assert.That(quest.Difficulty, Is.EqualTo(difficulty));
            Assert.That(quest.Encounters.Count, Is.EqualTo(Constants.Quests.EncountersPerQuest));

        }

        [Test]
        public void QuestsShouldContainRewards()
        {
            var factory = new QuestFactory(RandomnessProviderMock.Object, TimeProvider);
            var quest = factory.CreateQuest(Guid.NewGuid().ToString(), Difficulty.NORMAL);

            Assert.That(quest.Rewards.XP, Is.EqualTo(20));
        }
    }
}