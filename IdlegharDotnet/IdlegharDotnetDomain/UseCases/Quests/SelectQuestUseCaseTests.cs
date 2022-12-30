using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.UseCases;
using IdlegharDotnetDomain.UseCases.Quests;
using IdlegharDotnetShared.Quests;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Tests.UseCases.Quests
{
    public class SelectQuestUseCaseTests : BaseTests
    {
        [Test]
        public async Task GivenAValidUserAndAValidQuestShouldStartQuest()
        {
            User user = await CreateAndStoreUser(new Factories.UserFactoryOptions { Character = CreateCharacter() });

            var quests = await new GetAvailableQuestsUseCase(RandomnessProvider, QuestsProvider, TimeProvider).Handle();

            var useCase = new SelectQuestUseCase(UsersProvider, QuestsProvider, TimeProvider);
            await useCase.Handle(new AuthenticatedRequest<SelectQuestUseCaseRequest>(user, new SelectQuestUseCaseRequest(quests[0].Id)));

            var updatedUser = await UsersProvider.FindById(user.Id);
            Assert.NotNull(updatedUser!.Character!.CurrentQuest);
        }

        [Test]
        public async Task GivenAnInvalidUserItShouldFail()
        {
            User user = await CreateAndStoreUser();

            var quests = await new GetAvailableQuestsUseCase(RandomnessProvider, QuestsProvider, TimeProvider).Handle();

            var useCase = new SelectQuestUseCase(UsersProvider, QuestsProvider, TimeProvider);
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest<SelectQuestUseCaseRequest>(user, new SelectQuestUseCaseRequest(quests[0].Id)));
            });

            Assert.AreEqual(Constants.ErrorMessages.CHARACTER_NOT_CREATED, ex!.Message);

            var updatedUser = await UsersProvider.FindById(user.Id);
            Assert.IsNull(updatedUser!.Character);
        }

        [Test]
        public async Task GivenAnInvalidQuestItShouldFail()
        {
            User user = await CreateAndStoreUser(new Factories.UserFactoryOptions { Character = CreateCharacter() });

            var quests = await new GetAvailableQuestsUseCase(RandomnessProvider, QuestsProvider, TimeProvider).Handle();

            var useCase = new SelectQuestUseCase(UsersProvider, QuestsProvider, TimeProvider);
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest<SelectQuestUseCaseRequest>(user, new SelectQuestUseCaseRequest("wrongQuestId")));
            });

            Assert.AreEqual(Constants.ErrorMessages.INVALID_QUEST, ex!.Message);

            var updatedUser = await UsersProvider.FindById(user.Id);
            Assert.IsNull(updatedUser!.Character!.CurrentQuest);
        }

        [Test]
        public async Task GivenAnOldQuestItShouldFail()
        {
            User user = await CreateAndStoreUser(new Factories.UserFactoryOptions { Character = CreateCharacter() });

            var quests = await new GetAvailableQuestsUseCase(RandomnessProvider, QuestsProvider, TimeProvider).Handle();

            TimeProvider.MoveTimeInTicks(Constants.TimeDefinitions.QuestsRegenerationTimeInTicks);

            var useCase = new SelectQuestUseCase(UsersProvider, QuestsProvider, TimeProvider);
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest<SelectQuestUseCaseRequest>(user, new SelectQuestUseCaseRequest(quests[0].Id)));
            });

            Assert.AreEqual(Constants.ErrorMessages.INVALID_QUEST, ex!.Message);

            var updatedUser = await UsersProvider.FindById(user.Id);
            Assert.IsNull(updatedUser!.Character!.CurrentQuest);
        }
    }
}