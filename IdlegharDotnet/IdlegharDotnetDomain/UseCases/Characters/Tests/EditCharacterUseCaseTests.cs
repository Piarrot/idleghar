using IdlegharDotnetDomain.Tests;
using IdlegharDotnetDomain.Tests.Factories;
using IdlegharDotnetShared.Characters;
using NUnit.Framework;

namespace IdlegharDotnetDomain.UseCases.Characters.Tests
{
    public class EditCharacterUseCaseTests : BaseTests
    {
        [Test]
        public async Task GivenAUserAndAValidEditionRequestShouldEditTheCharacter()
        {
            var user = await FakeUserFactory.CreateAndStoreUserAndCharacter();

            var useCase = new EditCharacterUseCase(UsersProvider);

            var request = new EditCharacterUseCaseRequest("Nice Guy");

            var character = await useCase.Handle(new AuthenticatedRequest<EditCharacterUseCaseRequest>(user, request));

            Assert.That(character.Name, Is.EqualTo(request.Name));
        }

        [Test]
        public async Task GivenAUserWithoutCharacterShouldFail()
        {
            var user = await FakeUserFactory.CreateAndStoreUser();

            var useCase = new EditCharacterUseCase(UsersProvider);

            var request = new EditCharacterUseCaseRequest("Nice Guy");

            var e = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest<EditCharacterUseCaseRequest>(user, request));
            });
            Assert.That(e!.Message, Is.EqualTo(Constants.ErrorMessages.CHARACTER_NOT_CREATED));

        }
    }
}