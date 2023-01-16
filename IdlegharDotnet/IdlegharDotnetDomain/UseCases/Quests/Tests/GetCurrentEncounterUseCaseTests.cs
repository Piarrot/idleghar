using IdlegharDotnetDomain.Entities.Encounters;
using IdlegharDotnetDomain.Tests;
using NUnit.Framework;

namespace IdlegharDotnetDomain.UseCases.Quests.Tests
{
    public class GetCurrentEncounterUseCaseTests : BaseTests
    {
        [Test]
        public async Task GivenAValidPlayerAndACharacterWithCurrentEncounterShouldReturnCorrectEncounter()
        {
            var player = await FakePlayerFactory.CreateAndStorePlayerAndCharacterWithQuest();
            var character = await CharactersProvider.GetCharacterFromPlayerOrThrow(player);
            Encounter encounter = character.GetEncounterOrThrow();

            var useCase = new GetCurrentEncounterUseCase(PlayersProvider, CharactersProvider);
            Encounter result = await useCase.Handle(new AuthenticatedRequest(player));

            Assert.That(result, Is.EqualTo(encounter));
        }

        [Test]
        public async Task GivenAPlayerWithoutCharacterItShouldFail()
        {
            var player = await FakePlayerFactory.CreateAndStorePlayer();

            var useCase = new GetCurrentEncounterUseCase(PlayersProvider, CharactersProvider);
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest(player));
            });
            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.CHARACTER_NOT_CREATED));
        }

        [Test]
        public async Task GivenAPlayerWithCharacterWithoutAQuestItShouldFail()
        {
            var player = await FakePlayerFactory.CreateAndStorePlayerAndCharacter();

            var useCase = new GetCurrentEncounterUseCase(PlayersProvider, CharactersProvider);
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest(player));
            });
            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.CHARACTER_NOT_QUESTING));
        }
    }
}