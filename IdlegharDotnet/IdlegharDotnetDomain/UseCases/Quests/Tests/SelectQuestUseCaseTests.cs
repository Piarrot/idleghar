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
            var user = await FakeUserFactory.CreateAndStoreUserAndCharacter();
            var quests = await FakeQuestFactory.GetAvailableQuests();

            var useCase = new SelectQuestUseCase(UsersProvider, QuestsProvider, TimeProvider);
            await useCase.Handle(new AuthenticatedRequest<SelectQuestUseCaseRequest>(user, new SelectQuestUseCaseRequest(quests[0].Id)));

            var updatedUser = await UsersProvider.FindById(user.Id);
            Assert.IsTrue(updatedUser!.Character!.IsQuesting);
        }

        [Test]
        public async Task GivenAnInvalidUserItShouldFail()
        {
            var user = await FakeUserFactory.CreateAndStoreUser();
            var quests = await FakeQuestFactory.GetAvailableQuests();

            var useCase = new SelectQuestUseCase(UsersProvider, QuestsProvider, TimeProvider);
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest<SelectQuestUseCaseRequest>(user, new SelectQuestUseCaseRequest(quests[0].Id)));
            });

            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.CHARACTER_NOT_CREATED));
        }

        [Test]
        public async Task GivenAnInvalidQuestItShouldFail()
        {
            var user = await FakeUserFactory.CreateAndStoreUserAndCharacter();
            var quests = await FakeQuestFactory.GetAvailableQuests();

            var useCase = new SelectQuestUseCase(UsersProvider, QuestsProvider, TimeProvider);
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest<SelectQuestUseCaseRequest>(user, new SelectQuestUseCaseRequest("wrongQuestId")));
            });

            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.INVALID_QUEST));

            var updatedUser = await UsersProvider.FindById(user.Id);
            Assert.That(updatedUser!.Character!.IsQuesting, Is.False);
        }

        [Test]
        public async Task GivenAnOldQuestItShouldFail()
        {
            User user = await FakeUserFactory.CreateAndStoreUserAndCharacter();
            var quests = await FakeQuestFactory.GetAvailableQuests();

            TimeProvider.MoveTimeInTicks(Constants.TimeDefinitions.QuestsRegenerationTimeInTicks);

            var useCase = new SelectQuestUseCase(UsersProvider, QuestsProvider, TimeProvider);
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest<SelectQuestUseCaseRequest>(user, new SelectQuestUseCaseRequest(quests[0].Id)));
            });

            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.INVALID_QUEST));

            var updatedUser = await UsersProvider.FindById(user.Id);
            Assert.That(updatedUser!.Character!.IsQuesting, Is.False);
        }

        [Test]
        public async Task GivenACharacterAlreadyQuestingShouldFail()
        {
            User user = await FakeUserFactory.CreateAndStoreUserAndCharacterWithQuest();
            var quests = await FakeQuestFactory.GetAvailableQuests();
            Quest quest = user.Character!.GetCurrentQuestOrThrow();

            var useCase = new SelectQuestUseCase(UsersProvider, QuestsProvider, TimeProvider);
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest<SelectQuestUseCaseRequest>(user, new SelectQuestUseCaseRequest(quests[0].Id)));
            });

            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.CHARACTER_ALREADY_QUESTING));

            var updatedUser = await UsersProvider.FindById(user.Id);
            Assert.That(updatedUser!.Character!.IsQuesting, Is.True);
            Assert.That(updatedUser!.Character!.CurrentQuestState!.Quest, Is.EqualTo(quest));
        }
    }
}