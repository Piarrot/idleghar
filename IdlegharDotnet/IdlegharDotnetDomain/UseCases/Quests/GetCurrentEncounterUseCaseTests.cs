using IdlegharDotnetDomain.UseCases;
using IdlegharDotnetDomain.UseCases.Quests;
using IdlegharDotnetShared;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Tests.UseCases.Quests
{
    public class GetCurrentEncounterUseCaseTests : BaseTests
    {
        [Test]
        public async void GivenAValidUserItShouldReturnTheCurrentEncounterOfTheirCharacter()
        {
            var user = await CreateAndStoreUser(new Factories.UserFactoryOptions
            {
                Character = CreateCharacter()
            });

            var useCase = new GetCurrentEncounterUseCase();
            var result = useCase.Handle(new AuthenticatedRequest<EmptyRequest>(user, new EmptyRequest()));


        }
    }
}