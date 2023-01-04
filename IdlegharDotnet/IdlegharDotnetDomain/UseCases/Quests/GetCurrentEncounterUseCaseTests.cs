using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.UseCases;
using IdlegharDotnetDomain.UseCases.Quests;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Tests.UseCases.Quests
{
    public class GetCurrentEncounterUseCaseTests : BaseTests
    {

        [Test]
        public async Task GivenAValidUserAndACharacterWithCurrentEncounterShouldReturnCorrectEncounter()
        {
            var user = await CreateAndStoreUserAndCharacterWithQuest();
            Encounter encounter = user.Character!.CurrentEncounter!;

            var useCase = new GetCurrentEncounterUseCase(UsersProvider);
            Encounter result = useCase.Handle(new AuthenticatedRequest(user));

            Assert.AreEqual(result, encounter);
        }

        [Test]
        public async Task GivenAUserWithoutCharacterItShouldFail()
        {
            var user = await CreateAndStoreUser();

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
            var user = await CreateAndStoreUserAndCharacter();

            var useCase = new GetCurrentEncounterUseCase(UsersProvider);
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                useCase.Handle(new AuthenticatedRequest(user));
            });

            Assert.AreEqual(Constants.ErrorMessages.CHARACTER_NOT_QUESTING, ex!.Message);
        }
    }
}