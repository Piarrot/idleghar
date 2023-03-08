using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Tests;
using IdlegharDotnetShared.Quests;
using NUnit.Framework;

namespace IdlegharDotnetDomain.UseCases.Quests.Tests
{
    public class SelectQuestUseCaseTests : BaseTests
    {
        [Test]
        public async Task GivenAValidPlayerAndAValidQuestShouldStartQuest()
        {
            var player = await FakePlayerFactory.CreateAndStorePlayerAndCharacter();
            var quests = await FakeQuestFactory.GetAvailableQuests();

            var useCase = new SelectQuestUseCase(QuestsProvider, TimeProvider, StorageProvider);
            await useCase.Handle(new AuthenticatedRequest<SelectQuestUseCaseRequest>(player, new SelectQuestUseCaseRequest(quests[0].Id)));

            var updatedCharacter = await StorageProvider.GetCharacterByPlayerIdOrThrow(player.Id);
            Assert.IsTrue(updatedCharacter.IsQuesting);
        }

        [Test]
        public async Task GivenAnInvalidPlayerItShouldFail()
        {
            var player = await FakePlayerFactory.CreateAndStorePlayer();
            var quests = await FakeQuestFactory.GetAvailableQuests();

            var useCase = new SelectQuestUseCase(QuestsProvider, TimeProvider, StorageProvider);
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest<SelectQuestUseCaseRequest>(player, new SelectQuestUseCaseRequest(quests[0].Id)));
            });

            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.CHARACTER_NOT_CREATED));
        }

        [Test]
        public async Task GivenAnInvalidQuestItShouldFail()
        {
            var player = await FakePlayerFactory.CreateAndStorePlayerAndCharacter();
            var quests = await FakeQuestFactory.GetAvailableQuests();

            var useCase = new SelectQuestUseCase(QuestsProvider, TimeProvider, StorageProvider);
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest<SelectQuestUseCaseRequest>(player, new SelectQuestUseCaseRequest("wrongQuestId")));
            });

            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.INVALID_QUEST));

            var updatedCharacter = await StorageProvider.GetCharacterByPlayerIdOrThrow(player.Id);
            Assert.That(updatedCharacter.IsQuesting, Is.False);
        }

        [Test]
        public async Task GivenAnOldQuestItShouldFail()
        {
            Player player = await FakePlayerFactory.CreateAndStorePlayerAndCharacter();
            var quests = await FakeQuestFactory.GetAvailableQuests();

            TimeProvider.MoveTimeInTicks(Constants.TimeDefinitions.QuestsRegenerationTimeInTicks);

            var useCase = new SelectQuestUseCase(QuestsProvider, TimeProvider, StorageProvider);
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest<SelectQuestUseCaseRequest>(player, new SelectQuestUseCaseRequest(quests[0].Id)));
            });

            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.INVALID_QUEST));

            var updatedCharacter = await StorageProvider.GetCharacterByPlayerIdOrThrow(player.Id);
            Assert.That(updatedCharacter.IsQuesting, Is.False);
        }

        [Test]
        public async Task GivenACharacterAlreadyQuestingShouldFail()
        {
            Player player = await FakePlayerFactory.CreateAndStorePlayerAndCharacterWithQuest();
            var quests = await FakeQuestFactory.GetAvailableQuests();
            var character = await StorageProvider.GetCharacterByPlayerIdOrThrow(player.Id);
            Quest quest = character.GetCurrentQuestOrThrow();

            var useCase = new SelectQuestUseCase(QuestsProvider, TimeProvider, StorageProvider);
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest<SelectQuestUseCaseRequest>(player, new SelectQuestUseCaseRequest(quests[0].Id)));
            });

            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.CHARACTER_ALREADY_QUESTING));

            var updatedCharacter = await StorageProvider.GetCharacterByPlayerIdOrThrow(player.Id);
            Assert.That(updatedCharacter.IsQuesting, Is.True);
            Assert.That(updatedCharacter.CurrentQuestState!.Quest, Is.EqualTo(quest));
        }
    }
}