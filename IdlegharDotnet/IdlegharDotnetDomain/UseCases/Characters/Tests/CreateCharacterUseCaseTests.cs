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

            Assert.That(character, Is.Not.Null);
            Assert.That(character.Name, Is.EqualTo(request.Name));

            var updatedUser = await UsersProvider.FindById(user.Id);

            Assert.That(updatedUser!.Character, Is.Not.Null);
            Assert.That(character.Id, Is.EqualTo(updatedUser!.Character!.Id));
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
            Assert.That(e!.Message, Is.EqualTo(Constants.ErrorMessages.MORE_THAN_ONE_CHARACTER));
        }
    }
}