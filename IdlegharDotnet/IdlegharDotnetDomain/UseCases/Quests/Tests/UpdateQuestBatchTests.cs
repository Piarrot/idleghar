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

            var easyQuestCount = updatedQuestBatch!.Quests.Count(quest => quest.Difficulty == Constants.Difficulties.EASY);
            Assert.That(Constants.Quests.EasyQuestCount.Matches(easyQuestCount), Is.True);

            var normalQuestCount = updatedQuestBatch!.Quests.Count(quest => quest.Difficulty == Constants.Difficulties.NORMAL);
            Assert.That(Constants.Quests.NormalQuestCount.Matches(normalQuestCount), Is.True);

            var hardQuestCount = updatedQuestBatch!.Quests.Count(quest => quest.Difficulty == Constants.Difficulties.HARD);
            Assert.That(Constants.Quests.HardQuestCount.Matches(hardQuestCount), Is.True);

            var legendaryQuestCount = updatedQuestBatch!.Quests.Count(quest => quest.Difficulty == Constants.Difficulties.LEGENDARY);
            Assert.That(Constants.Quests.LegendaryQuestCount.Matches(legendaryQuestCount), Is.True);
        }
    }
}