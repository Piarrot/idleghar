using IdlegharDotnetDomain.Tests;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Factories.Tests
{
    public class QuestFactoryTests : BaseTests
    {
        [Test]
        public void CreatedQuestsShouldHaveEncounters()
        {
            var factory = new QuestFactory(RandomnessProvider, TimeProvider);
            var quest = factory.CreateQuest(Guid.NewGuid().ToString(), Constants.Difficulty.EASY);

            Assert.That(quest.Encounters.Count == Constants.Quests.EncountersPerQuest, Is.True);
        }

        [Test]
        public void CreatedQuestsShouldHaveEncountersOfManyDifficulties()
        {
            var factory = new QuestFactory(RandomnessProvider, TimeProvider);
            var quest = factory.CreateQuest(Guid.NewGuid().ToString(), Constants.Difficulty.NORMAL);

            var differentDifficultiesCount = quest.Encounters.Select((e) => e.Difficulty).Distinct().Count();
            Assert.That(differentDifficultiesCount, Is.GreaterThanOrEqualTo(2));
        }

        [Test]
        public void CreatedQuestsShouldConformWithSpecification()
        {
            var factory = new QuestFactory(RandomnessProvider, TimeProvider);
            var updatedQuestBatch = factory.CreateQuestBatch();

            foreach (Constants.Difficulty questDifficulty in Enum.GetValues(typeof(Constants.Difficulty)))
            {
                var questCount = updatedQuestBatch!.Quests.Count(quest => quest.Difficulty == questDifficulty);
                Assert.That(Constants.Quests.QuestCountByDifficulty[questDifficulty].Matches(questCount), Is.True);
            }
        }
    }
}