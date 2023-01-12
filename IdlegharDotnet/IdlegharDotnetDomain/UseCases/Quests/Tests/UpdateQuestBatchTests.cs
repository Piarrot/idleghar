using IdlegharDotnetDomain.Tests;
using NUnit.Framework;

namespace IdlegharDotnetDomain.UseCases.Quests.Tests
{
    public class UpdateQuestBatchTests : BaseTests
    {
        [Test]
        public async Task CreatedQuestsShouldConformWithSpecification()
        {
            // Specs: https://docs.google.com/document/d/1loOzBcBmZVcGhZWQc69hp5oNjqyMfc-GvOeIxjzq_9M/edit#heading=h.obgpyyfs06mo

            var useCase = new UpdateQuestBatch(RandomnessProvider, QuestsProvider, TimeProvider);
            await useCase.Handle();

            var updatedQuestBatch = await QuestsProvider.GetCurrentQuestBatch();

            var easyQuestCount = updatedQuestBatch!.Quests.Count(quest => quest.Difficulty == Constants.Difficulties.EASY);
            Assert.That(easyQuestCount, Is.GreaterThanOrEqualTo(1));
            Assert.That(easyQuestCount, Is.LessThanOrEqualTo(3));

            var normalQuestCount = updatedQuestBatch!.Quests.Count(quest => quest.Difficulty == Constants.Difficulties.NORMAL);
            Assert.That(normalQuestCount, Is.GreaterThanOrEqualTo(2));
            Assert.That(normalQuestCount, Is.LessThanOrEqualTo(4));

            var hardQuestCount = updatedQuestBatch!.Quests.Count(quest => quest.Difficulty == Constants.Difficulties.HARD);
            Assert.That(hardQuestCount, Is.GreaterThanOrEqualTo(1));
            Assert.That(hardQuestCount, Is.LessThanOrEqualTo(3));

            var legendaryQuestCount = updatedQuestBatch!.Quests.Count(quest => quest.Difficulty == Constants.Difficulties.LEGENDARY);
            Assert.That(legendaryQuestCount, Is.EqualTo(1));
        }
    }
}