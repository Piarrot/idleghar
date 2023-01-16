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
            Encounter encounter = player.Character!.GetEncounterOrThrow();

            var useCase = new GetCurrentEncounterUseCase(PlayersProvider);
            Encounter result = useCase.Handle(new AuthenticatedRequest(player));

            Assert.That(result, Is.EqualTo(encounter));
        }

        [Test]
        public async Task GivenAPlayerWithoutCharacterItShouldFail()
        {
            var player = await FakePlayerFactory.CreateAndStorePlayer();

            var useCase = new GetCurrentEncounterUseCase(PlayersProvider);
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                useCase.Handle(new AuthenticatedRequest(player));
            });
            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.CHARACTER_NOT_CREATED));
        }

        [Test]
        public async Task GivenAPlayerWithCharacterWithoutAQuestItShouldFail()
        {
            var player = await FakePlayerFactory.CreateAndStorePlayerAndCharacter();

            var useCase = new GetCurrentEncounterUseCase(PlayersProvider);
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                useCase.Handle(new AuthenticatedRequest(player));
            });
            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.CHARACTER_NOT_QUESTING));
        }
    }
}