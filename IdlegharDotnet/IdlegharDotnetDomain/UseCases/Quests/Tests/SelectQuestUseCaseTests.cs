using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Tests;
using IdlegharDotnetShared.Quests;
using NUnit.Framework;

namespace IdlegharDotnetDomain.UseCases.Quests.Tests
{
    public class SelectQuestUseCaseTests : BaseTests
    {
        [Test]
        public async Task GivenAValidUserAndAValidQuestShouldStartQuest()
        {
            var user = await CreateAndStoreUserAndCharacter();
            var quests = await GetAvailableQuests(user);

            var useCase = new SelectQuestUseCase(UsersProvider, QuestsProvider, TimeProvider);
            await useCase.Handle(new AuthenticatedRequest<SelectQuestUseCaseRequest>(user, new SelectQuestUseCaseRequest(quests[0].Id)));

            var updatedUser = await UsersProvider.FindById(user.Id);
            Assert.IsTrue(updatedUser!.Character!.IsQuesting);
        }

        [Test]
        public async Task GivenAnInvalidUserItShouldFail()
        {
            var user = await CreateAndStoreUser();
            var quests = await GetAvailableQuests(user);

            var useCase = new SelectQuestUseCase(UsersProvider, QuestsProvider, TimeProvider);
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest<SelectQuestUseCaseRequest>(user, new SelectQuestUseCaseRequest(quests[0].Id)));
            });

            Assert.AreEqual(Constants.ErrorMessages.CHARACTER_NOT_CREATED, ex!.Message);
        }

        [Test]
        public async Task GivenAnInvalidQuestItShouldFail()
        {
            var user = await CreateAndStoreUserAndCharacter();
            var quests = await GetAvailableQuests(user);

            var useCase = new SelectQuestUseCase(UsersProvider, QuestsProvider, TimeProvider);
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest<SelectQuestUseCaseRequest>(user, new SelectQuestUseCaseRequest("wrongQuestId")));
            });

            Assert.AreEqual(Constants.ErrorMessages.INVALID_QUEST, ex!.Message);

            var updatedUser = await UsersProvider.FindById(user.Id);
            Assert.IsFalse(updatedUser!.Character!.IsQuesting);
        }

        [Test]
        public async Task GivenAnOldQuestItShouldFail()
        {
            User user = await CreateAndStoreUserAndCharacter();
            var quests = await GetAvailableQuests(user);

            TimeProvider.MoveTimeInTicks(Constants.TimeDefinitions.QuestsRegenerationTimeInTicks);

            var useCase = new SelectQuestUseCase(UsersProvider, QuestsProvider, TimeProvider);
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest<SelectQuestUseCaseRequest>(user, new SelectQuestUseCaseRequest(quests[0].Id)));
            });

            Assert.AreEqual(Constants.ErrorMessages.INVALID_QUEST, ex!.Message);

            var updatedUser = await UsersProvider.FindById(user.Id);
            Assert.IsFalse(updatedUser!.Character!.IsQuesting);
        }

        [Test]
        public async Task GivenACharacterAlreadyQuestingShouldFail()
        {
            User user = await CreateAndStoreUserAndCharacterWithQuest();
            var quests = await GetAvailableQuests(user);
            Quest quest = user.Character!.CurrentQuest!;

            var useCase = new SelectQuestUseCase(UsersProvider, QuestsProvider, TimeProvider);
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest<SelectQuestUseCaseRequest>(user, new SelectQuestUseCaseRequest(quests[0].Id)));
            });

            Assert.AreEqual(Constants.ErrorMessages.CHARACTER_ALREADY_QUESTING, ex!.Message);

            var updatedUser = await UsersProvider.FindById(user.Id);
            Assert.IsTrue(updatedUser!.Character!.IsQuesting);
            Assert.AreEqual(quest, updatedUser!.Character!.CurrentQuest);
        }
    }
}