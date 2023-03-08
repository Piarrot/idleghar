using IdlegharDotnetDomain.Tests;
using IdlegharDotnetDomain.Tests.Factories;
using IdlegharDotnetShared.Characters;
using NUnit.Framework;

namespace IdlegharDotnetDomain.UseCases.Characters.Tests
{
    public class CreateCharacterUseCaseTests : BaseTests
    {
        [Test]
        public async Task GivenANameAndAValidPlayerShouldCreateACharacter()
        {
            var player = await FakePlayerFactory.CreateAndStorePlayer();
            var useCase = new CreateCharacterUseCase(StorageProvider);

            var request = new CreateCharacterUseCaseRequest("Cool Character");

            var character = await useCase.Handle(new AuthenticatedRequest<CreateCharacterUseCaseRequest>(player, request));

            Assert.That(character, Is.Not.Null);
            Assert.That(character.Name, Is.EqualTo(request.Name));

            var updatedCharacter = await StorageProvider.GetCharacterByPlayerIdOrThrow(player.Id);

            Assert.That(updatedCharacter, Is.Not.Null);
            Assert.That(character.Id, Is.EqualTo(updatedCharacter!.Id));
        }

        [Test]
        public async Task GivenAPlayerWithACharacterShouldFailToCreateANewCharacter()
        {
            var player = await FakePlayerFactory.CreateAndStorePlayerAndCharacter();

            var request = new CreateCharacterUseCaseRequest("Cool Character");
            var useCase = new CreateCharacterUseCase(StorageProvider);

            var e = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest<CreateCharacterUseCaseRequest>(player, request));
            });
            Assert.That(e!.Message, Is.EqualTo(Constants.ErrorMessages.MORE_THAN_ONE_CHARACTER));
        }
    }
}