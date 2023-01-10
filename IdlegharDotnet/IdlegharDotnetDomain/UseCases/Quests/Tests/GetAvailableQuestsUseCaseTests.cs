using IdlegharDotnetDomain.Tests;
using NUnit.Framework;

namespace IdlegharDotnetDomain.UseCases.Quests.Tests
{

    public class GetAvailableQuestsUseCaseTests : BaseTests
    {
        [Test]
        public async Task GivenAUserItShouldListAvailableQuests()
        {
            var user = await FakeUserFactory.CreateAndStoreUserAndCharacter();

            var useCase = new GetAvailableQuestsUseCase(RandomnessProvider, QuestsProvider, TimeProvider);
            var quests = await useCase.Handle(new AuthenticatedRequest(user));

            var easyQuestCount = quests.Count(quest => quest.Difficulty == Constants.Difficulties.EASY);
            Assert.That(easyQuestCount, Is.GreaterThanOrEqualTo(1));
            Assert.That(easyQuestCount, Is.LessThanOrEqualTo(3));

            var normalQuestCount = quests.Count(quest => quest.Difficulty == Constants.Difficulties.NORMAL);
            Assert.That(normalQuestCount, Is.GreaterThanOrEqualTo(2));
            Assert.That(normalQuestCount, Is.LessThanOrEqualTo(4));

            var hardQuestCount = quests.Count(quest => quest.Difficulty == Constants.Difficulties.HARD);
            Assert.That(hardQuestCount, Is.GreaterThanOrEqualTo(1));
            Assert.That(hardQuestCount, Is.LessThanOrEqualTo(3));

            var legendaryQuestCount = quests.Count(quest => quest.Difficulty == Constants.Difficulties.LEGENDARY);
            Assert.That(legendaryQuestCount, Is.EqualTo(1));
        }

        [Test]
        public async Task GivenManyConsecutiveRequestsShouldListTheSameAvailableQuests()
        {
            var user = await FakeUserFactory.CreateAndStoreUserAndCharacter();
            var useCase = new GetAvailableQuestsUseCase(RandomnessProvider, QuestsProvider, TimeProvider);
            var firstQuestsBatch = await useCase.Handle(new AuthenticatedRequest(user));
            var secondQuestsBatch = await useCase.Handle(new AuthenticatedRequest(user));

            Assert.That(secondQuestsBatch.Count, Is.EqualTo(firstQuestsBatch.Count));
            Assert.That(secondQuestsBatch[0].Id, Is.EqualTo(firstQuestsBatch[0].Id));
            Assert.That(secondQuestsBatch[firstQuestsBatch.Count - 1].Id, Is.EqualTo(firstQuestsBatch[firstQuestsBatch.Count - 1].Id));
        }

        [Test]
        public async Task GivenEnoughTicksItShouldListNewQuests()
        {
            var user = await FakeUserFactory.CreateAndStoreUserAndCharacter();

            var useCase = new GetAvailableQuestsUseCase(RandomnessProvider, QuestsProvider, TimeProvider);

            var firstQuestsBatch = await useCase.Handle(new AuthenticatedRequest(user));

            TimeProvider.MoveTimeInTicks(Constants.TimeDefinitions.QuestsRegenerationTimeInTicks);

            var secondQuestsBatch = await useCase.Handle(new AuthenticatedRequest(user));

            foreach (var q1 in firstQuestsBatch)
            {
                Assert.That(secondQuestsBatch.All(q2 => q1.Id != q2.Id), Is.True, "All quests should have been regenerated");
            }
        }

        [Test]
        public async Task CreatedQuestsShouldHaveEncounters()
        {
            var user = await FakeUserFactory.CreateAndStoreUserAndCharacter();
            var useCase = new GetAvailableQuestsUseCase(RandomnessProvider, QuestsProvider, TimeProvider);

            await useCase.Handle(new AuthenticatedRequest(user));

            var quests = (await QuestsProvider.GetCurrentQuestBatch())!.Quests;

            Assert.That(quests.All((quest) =>
            {
                return quest.Encounters.Count == Constants.Quests.EncountersPerQuest;
            }), Is.True);

        }
    }
}