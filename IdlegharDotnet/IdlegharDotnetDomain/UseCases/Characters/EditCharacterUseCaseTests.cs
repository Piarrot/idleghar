using IdlegharDotnetDomain.Tests.Factories;
using IdlegharDotnetDomain.UseCases;
using IdlegharDotnetDomain.UseCases.Characters;
using IdlegharDotnetShared.Character;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Tests.UseCases.Characters
{
    public class EditCharacterUseCaseTests : BaseTests
    {
        [Test]
        public async Task GivenAUserAndAValidEditionRequestShouldEditTheCharacter()
        {
            var user = await this.CreateAndStoreUser(new UserFactoryOptions
            {
                Character = CreateCharacter("Cool Guy")
            });

            var useCase = new EditCharacterUseCase(UsersProvider);

            var request = new EditCharacterUseCaseRequest
            {
                Name = "Nice Guy"
            };

            var character = await useCase.Handle(new AuthenticatedRequest<EditCharacterUseCaseRequest>
            {
                CurrentUser = user,
                Request = request
            });

            Assert.AreEqual(request.Name, character.Name);
        }

        [Test]
        public async Task GivenAUserWithoutCharacterShouldFail()
        {
            var user = await this.CreateAndStoreUser();

            var useCase = new EditCharacterUseCase(UsersProvider);

            var request = new EditCharacterUseCaseRequest
            {
                Name = "Nice Guy"
            };

            var e = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest<EditCharacterUseCaseRequest>
                {
                    CurrentUser = user,
                    Request = request
                });
            });

            Assert.AreEqual(Constants.ErrorMessages.EDIT_CHARACTER_NOT_CREATED, e?.Message);

        }
    }
}