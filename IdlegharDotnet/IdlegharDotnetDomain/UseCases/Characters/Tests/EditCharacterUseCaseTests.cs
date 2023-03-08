using IdlegharDotnetDomain.Tests;
using IdlegharDotnetDomain.Tests.Factories;
using IdlegharDotnetShared.Characters;
using NUnit.Framework;

namespace IdlegharDotnetDomain.UseCases.Characters.Tests
{
    public class EditCharacterUseCaseTests : BaseTests
    {
        [Test]
        public async Task GivenAPlayerAndAValidEditionRequestShouldEditTheCharacter()
        {
            var player = await FakePlayerFactory.CreateAndStorePlayerAndCharacter();

            var useCase = new EditCharacterUseCase(StorageProvider);

            var request = new EditCharacterUseCaseRequest("Nice Guy");

            var character = await useCase.Handle(new AuthenticatedRequest<EditCharacterUseCaseRequest>(player, request));

            Assert.That(character.Name, Is.EqualTo(request.Name));
        }

        [Test]
        public async Task GivenAPlayerWithoutCharacterShouldFail()
        {
            var player = await FakePlayerFactory.CreateAndStorePlayer();

            var useCase = new EditCharacterUseCase(StorageProvider);

            var request = new EditCharacterUseCaseRequest("Nice Guy");

            var e = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest<EditCharacterUseCaseRequest>(player, request));
            });
            Assert.That(e!.Message, Is.EqualTo(Constants.ErrorMessages.CHARACTER_NOT_CREATED));

        }
    }
}