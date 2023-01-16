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

            var useCase = new SelectQuestUseCase(PlayersProvider, QuestsProvider, TimeProvider);
            await useCase.Handle(new AuthenticatedRequest<SelectQuestUseCaseRequest>(player, new SelectQuestUseCaseRequest(quests[0].Id)));

            var updatedPlayer = await PlayersProvider.FindById(player.Id);
            Assert.IsTrue(updatedPlayer!.Character!.IsQuesting);
        }

        [Test]
        public async Task GivenAnInvalidPlayerItShouldFail()
        {
            var player = await FakePlayerFactory.CreateAndStorePlayer();
            var quests = await FakeQuestFactory.GetAvailableQuests();

            var useCase = new SelectQuestUseCase(PlayersProvider, QuestsProvider, TimeProvider);
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

            var useCase = new SelectQuestUseCase(PlayersProvider, QuestsProvider, TimeProvider);
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest<SelectQuestUseCaseRequest>(player, new SelectQuestUseCaseRequest("wrongQuestId")));
            });

            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.INVALID_QUEST));

            var updatedPlayer = await PlayersProvider.FindById(player.Id);
            Assert.That(updatedPlayer!.Character!.IsQuesting, Is.False);
        }

        [Test]
        public async Task GivenAnOldQuestItShouldFail()
        {
            Player player = await FakePlayerFactory.CreateAndStorePlayerAndCharacter();
            var quests = await FakeQuestFactory.GetAvailableQuests();

            TimeProvider.MoveTimeInTicks(Constants.TimeDefinitions.QuestsRegenerationTimeInTicks);

            var useCase = new SelectQuestUseCase(PlayersProvider, QuestsProvider, TimeProvider);
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest<SelectQuestUseCaseRequest>(player, new SelectQuestUseCaseRequest(quests[0].Id)));
            });

            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.INVALID_QUEST));

            var updatedPlayer = await PlayersProvider.FindById(player.Id);
            Assert.That(updatedPlayer!.Character!.IsQuesting, Is.False);
        }

        [Test]
        public async Task GivenACharacterAlreadyQuestingShouldFail()
        {
            Player player = await FakePlayerFactory.CreateAndStorePlayerAndCharacterWithQuest();
            var quests = await FakeQuestFactory.GetAvailableQuests();
            Quest quest = player.Character!.GetCurrentQuestOrThrow();

            var useCase = new SelectQuestUseCase(PlayersProvider, QuestsProvider, TimeProvider);
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest<SelectQuestUseCaseRequest>(player, new SelectQuestUseCaseRequest(quests[0].Id)));
            });

            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.CHARACTER_ALREADY_QUESTING));

            var updatedPlayer = await PlayersProvider.FindById(player.Id);
            Assert.That(updatedPlayer!.Character!.IsQuesting, Is.True);
            Assert.That(updatedPlayer!.Character!.CurrentQuestState!.Quest, Is.EqualTo(quest));
        }
    }
}