using IdlegharDotnetDomain.Tests;
using NUnit.Framework;

namespace IdlegharDotnetDomain.UseCases.Quests.Tests
{

    public class GetAvailableQuestsUseCaseTests : BaseTests
    {
        [Test]
        public async Task GivenManyConsecutiveRequestsShouldListTheSameAvailableQuests()
        {
            var useCase = new GetAvailableQuestsUseCase(RandomnessProviderMock, QuestsProvider, TimeProvider);
            var firstQuestsBatch = await useCase.Handle();
            var secondQuestsBatch = await useCase.Handle();

            Assert.That(secondQuestsBatch.Count, Is.EqualTo(firstQuestsBatch.Count));
            Assert.That(secondQuestsBatch[0].Id, Is.EqualTo(firstQuestsBatch[0].Id));
            Assert.That(secondQuestsBatch[firstQuestsBatch.Count - 1].Id, Is.EqualTo(firstQuestsBatch[firstQuestsBatch.Count - 1].Id));
        }

        [Test]
        public async Task GivenEnoughTicksItShouldListNewQuests()
        {
            var useCase = new GetAvailableQuestsUseCase(RandomnessProviderMock, QuestsProvider, TimeProvider);

            var firstQuestsBatch = await useCase.Handle();

            TimeProvider.MoveTimeInTicks(Constants.TimeDefinitions.QuestsRegenerationTimeInTicks);

            var secondQuestsBatch = await useCase.Handle();

            foreach (var q1 in firstQuestsBatch)
            {
                Assert.That(secondQuestsBatch.All(q2 => q1.Id != q2.Id), Is.True, "All quests should have been regenerated");
            }
        }
    }
}