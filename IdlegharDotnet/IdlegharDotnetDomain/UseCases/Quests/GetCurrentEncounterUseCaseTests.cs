using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.UseCases;
using IdlegharDotnetDomain.UseCases.Quests;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Tests.UseCases.Quests
{
    public class GetCurrentEncounterUseCaseTests : BaseTests
    {
        [Test]
        public async Task GivenAValidUserItShouldReturnTheCurrentEncounterOfTheirCharacter()
        {
            var user = await CreateAndStoreUserAndCharacterWithQuest();
            var useCase = new GetCurrentEncounterUseCase(UsersProvider);
            var result = await useCase.Handle(new AuthenticatedRequest(user));
            Assert.AreEqual(typeof(Encounter), result.GetType());

            var updatedUser = await UsersProvider.FindById(user.Id);
            Assert.AreEqual(result, updatedUser!.Character!.CurrentEncounter);
        }

        [Test]
        public async Task GivenAValidUserAndACharacterWithCurrentEncounterShouldReturnCorrectQuest()
        {
            var user = await CreateAndStoreUserAndCharacterWithQuestAndEncounter();
            var encounter = user.Character!.CurrentEncounter;

            var useCase = new GetCurrentEncounterUseCase(UsersProvider);
            var result = await useCase.Handle(new AuthenticatedRequest(user));

            Assert.AreEqual(result, encounter);
        }

        [Test]
        public async Task GivenAUserWithoutCharacterItShouldFail()
        {
            var user = await CreateAndStoreUser();

            var useCase = new GetCurrentEncounterUseCase(UsersProvider);
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest(user));
            });

            Assert.AreEqual(Constants.ErrorMessages.CHARACTER_NOT_CREATED, ex.Message);
        }

        [Test]
        public async Task GivenAUserWithCharacterWithoutAQuestItShouldFail()
        {
            var user = await CreateAndStoreUserAndCharacter();

            var useCase = new GetCurrentEncounterUseCase(UsersProvider);
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest(user));
            });

            Assert.AreEqual(Constants.ErrorMessages.CHARACTER_NOT_QUESTING, ex.Message);
        }
    }
}