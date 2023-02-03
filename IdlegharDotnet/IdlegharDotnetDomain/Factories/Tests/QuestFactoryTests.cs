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
            var factory = new QuestFactory(RandomnessProviderMock, TimeProvider);
            var quest = factory.CreateQuest(Guid.NewGuid().ToString(), Difficulty.EASY);

            Assert.That(quest.Encounters.Count == Constants.Quests.EncountersPerQuest, Is.True);
        }

        [Test]
        public void CreatedQuestsShouldHaveEncountersOfManyDifficulties()
        {
            var factory = new QuestFactory(RandomnessProviderMock, TimeProvider);
            var quests = factory.CreateQuests(Guid.NewGuid().ToString(), Difficulty.NORMAL, 1000);

            var generatedEncounterDifficulties = quests.SelectMany((q) => q.Encounters.Select((e) => e.Difficulty)).Distinct();

            Assert.That(generatedEncounterDifficulties.Contains(Difficulty.EASY), Is.True);
            Assert.That(generatedEncounterDifficulties.Contains(Difficulty.NORMAL), Is.True);
            Assert.That(generatedEncounterDifficulties.Contains(Difficulty.HARD), Is.True);
        }

        [Test]
        public void CreatedQuestsShouldConformWithSpecification()
        {
            var factory = new QuestFactory(RandomnessProviderMock, TimeProvider);
            var updatedQuestBatch = factory.CreateQuestBatch();

            foreach (Difficulty questDifficulty in Enum.GetValues(typeof(Difficulty)))
            {
                var questCount = updatedQuestBatch!.Quests.Count(quest => quest.Difficulty == questDifficulty);
                Assert.That(Constants.Quests.QuestCountByDifficulty[questDifficulty].Matches(questCount), Is.True);
            }
        }

        [Test]
        public void QuestsShouldContainRewards()
        {
            var factory = new QuestFactory(RandomnessProviderMock, TimeProvider);
            var quest = factory.CreateQuest(Guid.NewGuid().ToString(), Difficulty.EASY);

            Assert.That(quest.Rewards.Count, Is.GreaterThan(0));
        }
    }
}