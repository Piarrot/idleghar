using IdlegharDotnetDomain.Tests;
using IdlegharDotnetShared.Constants;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Factories.Tests
{
    public class QuestFactoryTests : BaseTests
    {
        [Test]
        public void CreatedQuestsShouldHaveEncounters()
        {
            var factory = new QuestFactory(RandomnessProviderMock.Object, TimeProvider);
            var quest = factory.CreateQuest(Guid.NewGuid().ToString(), Difficulty.EASY);

            Assert.That(quest.Encounters.Count == Constants.Quests.EncountersPerQuest, Is.True);
        }

        [Test]
        public void CreatedQuestsShouldHaveEncountersOfManyDifficulties()
        {
            RandomnessProviderMock.SetupSequence(MockRandomDoubleLambda).Returns(0.1).Returns(0.4).Returns(0.9);

            var factory = new QuestFactory(RandomnessProviderMock.Object, TimeProvider);
            var quest = factory.CreateQuest(Guid.NewGuid().ToString(), Difficulty.NORMAL);

            var generatedEncounterDifficulties = quest.Encounters.Select((e) => e.Difficulty).Distinct();

            Assert.That(generatedEncounterDifficulties.Contains(Difficulty.EASY), Is.True);
            Assert.That(generatedEncounterDifficulties.Contains(Difficulty.NORMAL), Is.True);
            Assert.That(generatedEncounterDifficulties.Contains(Difficulty.HARD), Is.True);
        }

        [Test]
        public void QuestsShouldContainRewards()
        {
            var factory = new QuestFactory(RandomnessProviderMock.Object, TimeProvider);
            var quest = factory.CreateQuest(Guid.NewGuid().ToString(), Difficulty.EASY);

            Assert.That(quest.Rewards.Count, Is.GreaterThan(0));
        }
    }
}