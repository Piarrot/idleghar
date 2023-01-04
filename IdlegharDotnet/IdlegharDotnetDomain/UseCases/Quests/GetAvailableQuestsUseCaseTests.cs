using IdlegharDotnetDomain.UseCases;
using IdlegharDotnetDomain.UseCases.Quests;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Tests.UseCases.Quests
{

    public class GetAvailableQuestsUseCaseTests : BaseTests
    {
        [Test]
        public async Task GivenAUserItShouldListAvailableQuests()
        {
            var user = await CreateAndStoreUserAndCharacter();

            var useCase = new GetAvailableQuestsUseCase(RandomnessProvider, QuestsProvider, TimeProvider);
            var quests = await useCase.Handle(new AuthenticatedRequest(user));

            var easyQuestCount = quests.Count(quest => quest.Difficulty == Constants.Difficulties.EASY);
            Assert.GreaterOrEqual(easyQuestCount, 1);
            Assert.LessOrEqual(easyQuestCount, 3);

            var normalQuestCount = quests.Count(quest => quest.Difficulty == Constants.Difficulties.NORMAL);
            Assert.GreaterOrEqual(normalQuestCount, 2);
            Assert.LessOrEqual(normalQuestCount, 4);

            var hardQuestCount = quests.Count(quest => quest.Difficulty == Constants.Difficulties.HARD);
            Assert.GreaterOrEqual(hardQuestCount, 1);
            Assert.LessOrEqual(hardQuestCount, 3);

            var legendaryQuestCount = quests.Count(quest => quest.Difficulty == Constants.Difficulties.LEGENDARY);
            Assert.AreEqual(1, legendaryQuestCount);
        }

        [Test]
        public async Task GivenManyConsecutiveRequestsShouldListTheSameAvailableQuests()
        {
            var user = await CreateAndStoreUserAndCharacter();
            var useCase = new GetAvailableQuestsUseCase(RandomnessProvider, QuestsProvider, TimeProvider);
            var firstQuestsBatch = await useCase.Handle(new AuthenticatedRequest(user));
            var secondQuestsBatch = await useCase.Handle(new AuthenticatedRequest(user));

            Assert.AreEqual(firstQuestsBatch.Count, secondQuestsBatch.Count);
            Assert.AreEqual(firstQuestsBatch[0].Id, secondQuestsBatch[0].Id);
            Assert.AreEqual(firstQuestsBatch[firstQuestsBatch.Count - 1].Id, secondQuestsBatch[firstQuestsBatch.Count - 1].Id);
        }

        [Test]
        public async Task GivenEnoughTicksItShouldListNewQuests()
        {
            var user = await CreateAndStoreUserAndCharacter();

            var useCase = new GetAvailableQuestsUseCase(RandomnessProvider, QuestsProvider, TimeProvider);

            var firstQuestsBatch = await useCase.Handle(new AuthenticatedRequest(user));

            TimeProvider.MoveTimeInTicks(Constants.TimeDefinitions.QuestsRegenerationTimeInTicks);

            var secondQuestsBatch = await useCase.Handle(new AuthenticatedRequest(user));

            foreach (var q1 in firstQuestsBatch)
            {
                Assert.IsTrue(secondQuestsBatch.All(q2 => q1.Id != q2.Id), "Quests should have been regenerated");
            }
        }

        [Test]
        public async Task CreatedQuestsShouldHaveEncounters()
        {
            var user = await CreateAndStoreUserAndCharacter();
            var useCase = new GetAvailableQuestsUseCase(RandomnessProvider, QuestsProvider, TimeProvider);

            await useCase.Handle(new AuthenticatedRequest(user));

            var quests = (await QuestsProvider.GetCurrentQuestBatch())!.Quests;

            Assert.IsTrue(quests.All((quest) =>
            {
                return quest.Encounters.Count > 0;
            }));

        }
    }
}