using IdlegharDotnetDomain.Tests;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Factories.Tests
{
    public class QuestFactoryTests : BaseTests
    {
        [Test]
        public void CreatedQuestsShouldHaveEncounters()
        {
            var quest = QuestFactory.CreateQuestOfDifficulty(Guid.NewGuid().ToString(), Constants.Difficulties.EASY);

            Assert.That(quest.Encounters.Count == Constants.Quests.EncountersPerQuest, Is.True);
        }
    }
}