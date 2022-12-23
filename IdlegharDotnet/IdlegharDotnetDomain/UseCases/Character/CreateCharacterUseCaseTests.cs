using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Exceptions;
using IdlegharDotnetDomain.Tests.Factories;
using IdlegharDotnetDomain.UseCases;
using IdlegharDotnetDomain.UseCases.Characters;
using IdlegharDotnetShared.Character;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Tests.UseCases.Characters
{
    public class CreateCharacterUseCaseTests : BaseTests
    {
        [Test]
        public async Task GivenANameAndAValidUserShouldCreateACharacter()
        {
            var user = await this.CreateAndStoreUser();
            var useCase = new CreateCharacterUseCase(UsersProvider);

            var request = new CreateCharacterUseCaseRequest
            {
                Name = "CoolCharacter"
            };

            var character = await useCase.Handle(new AuthenticatedRequest<CreateCharacterUseCaseRequest>
            {
                CurrentUser = user,
                Request = request
            });

            Assert.NotNull(character);
            Assert.AreEqual(request.Name, character.Name);

            var updatedUser = await UsersProvider.FindById(user.Id);

            Assert.NotNull(updatedUser?.Character);
            Assert.AreEqual(updatedUser?.Character?.Id, character.Id);
        }

        [Test]
        public async Task GivenAUserWithACharacterShouldFailToCreateANewCharacter()
        {
            var user = await this.CreateAndStoreUser(new UserFactoryOptions
            {
                Character = CreateCharacter()
            });
            var request = new CreateCharacterUseCaseRequest
            {
                Name = "CoolCharacter"
            };
            var useCase = new CreateCharacterUseCase(UsersProvider);

            Assert.ThrowsAsync<MoreThanOneCharacterException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest<CreateCharacterUseCaseRequest>
                {
                    CurrentUser = user,
                    Request = request
                });
            });
        }
    }
}