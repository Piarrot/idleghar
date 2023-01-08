using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Tests;
using NUnit.Framework;

namespace IdlegharDotnetDomain.UseCases.Quests.Tests
{
    public class GetCurrentEncounterUseCaseTests : BaseTests
    {

        [Test]
        public async Task GivenAValidUserAndACharacterWithCurrentEncounterShouldReturnCorrectEncounter()
        {
            var user = await FakeUserFactory.CreateAndStoreUserAndCharacterWithQuest();
            Encounter encounter = user.Character!.CurrentEncounterState.Encounter!;

            var useCase = new GetCurrentEncounterUseCase(UsersProvider);
            Encounter result = useCase.Handle(new AuthenticatedRequest(user));

            Assert.AreEqual(result, encounter);
        }

        [Test]
        public async Task GivenAUserWithoutCharacterItShouldFail()
        {
            var user = await FakeUserFactory.CreateAndStoreUser();

            var useCase = new GetCurrentEncounterUseCase(UsersProvider);
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                useCase.Handle(new AuthenticatedRequest(user));
            });

            Assert.AreEqual(Constants.ErrorMessages.CHARACTER_NOT_CREATED, ex!.Message);
        }

        [Test]
        public async Task GivenAUserWithCharacterWithoutAQuestItShouldFail()
        {
            var user = await FakeUserFactory.CreateAndStoreUserAndCharacter();

            var useCase = new GetCurrentEncounterUseCase(UsersProvider);
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                useCase.Handle(new AuthenticatedRequest(user));
            });

            Assert.AreEqual(Constants.ErrorMessages.CHARACTER_NOT_QUESTING, ex!.Message);
        }
    }
}