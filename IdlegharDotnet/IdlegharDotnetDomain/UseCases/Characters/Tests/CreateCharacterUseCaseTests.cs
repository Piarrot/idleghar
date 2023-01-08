using IdlegharDotnetDomain.Tests;
using IdlegharDotnetDomain.Tests.Factories;
using IdlegharDotnetShared.Characters;
using NUnit.Framework;

namespace IdlegharDotnetDomain.UseCases.Characters.Tests
{
    public class CreateCharacterUseCaseTests : BaseTests
    {
        [Test]
        public async Task GivenANameAndAValidUserShouldCreateACharacter()
        {
            var user = await FakeUserFactory.CreateAndStoreUser();
            var useCase = new CreateCharacterUseCase(UsersProvider);

            var request = new CreateCharacterUseCaseRequest("Cool Character");

            var character = await useCase.Handle(new AuthenticatedRequest<CreateCharacterUseCaseRequest>(user, request));

            Assert.NotNull(character);
            Assert.AreEqual(request.Name, character.Name);

            var updatedUser = await UsersProvider.FindById(user.Id);

            Assert.NotNull(updatedUser!.Character);
            Assert.AreEqual(updatedUser!.Character!.Id, character.Id);
        }

        [Test]
        public async Task GivenAUserWithACharacterShouldFailToCreateANewCharacter()
        {
            var user = await FakeUserFactory.CreateAndStoreUserAndCharacter();

            var request = new CreateCharacterUseCaseRequest("Cool Character");
            var useCase = new CreateCharacterUseCase(UsersProvider);

            var e = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest<CreateCharacterUseCaseRequest>(user, request));
            });

            Assert.AreEqual(Constants.ErrorMessages.MORE_THAN_ONE_CHARACTER, e?.Message);
        }
    }
}