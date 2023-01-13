using IdlegharDotnetDomain.Tests;
using NUnit.Framework;

namespace IdlegharDotnetDomain.UseCases.Quests.Tests
{
    public class UpdateQuestBatchTests : BaseTests
    {
        [Test]
        public async Task CreatedQuestsShouldConformWithSpecification()
        {
            var useCase = new UpdateQuestBatch(RandomnessProvider, QuestsProvider, TimeProvider);
            await useCase.Handle();

            var updatedQuestBatch = await QuestsProvider.GetCurrentQuestBatch();

            foreach (Constants.Difficulty questDifficulty in Enum.GetValues(typeof(Constants.Difficulty)))
            {
                var questCount = updatedQuestBatch!.Quests.Count(quest => quest.Difficulty == questDifficulty);
                Assert.That(Constants.Quests.QuestCountByDifficulty[questDifficulty].Matches(questCount), Is.True);
            }
        }
    }
}