using IdlegharDotnetDomain.Tests;
using IdlegharDotnetShared.Characters;
using IdlegharDotnetShared.SharedConstants;
using NUnit.Framework;

namespace IdlegharDotnetDomain.UseCases.Characters.Tests
{
    public class LevelUpCharacterUseCaseTests : BaseTests
    {
        [Test]
        public async Task APlayerCannotTriggerACharacterToLevelUpWithNoCharacter()
        {
            var useCase = new LevelUpCharacterUseCase(StorageProvider);
            var player = await FakePlayerFactory.CreateAndStorePlayer();
            var e = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest<LevelUpCharacterUseCaseRequest>(player, new(new()
                {
                    [CharacterStat.TOUGHNESS] = 3
                })));
            });

            Assert.That(e!.Message, Is.EqualTo(Constants.ErrorMessages.CHARACTER_NOT_CREATED));
        }

        [Test]
        public async Task APlayerCannotTriggerACharacterToLevelUpIfCharacterHasNotEnoughXP()
        {
            var useCase = new LevelUpCharacterUseCase(StorageProvider);
            var player = await FakePlayerFactory.CreateAndStorePlayerAndCharacter();
            var e = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest<LevelUpCharacterUseCaseRequest>(player, new(new()
                {
                    [CharacterStat.TOUGHNESS] = 3
                })));
            });

            Assert.That(e!.Message, Is.EqualTo(Constants.ErrorMessages.CHARACTER_IS_NOT_LEVELING_UP));
        }

        [Test]
        public async Task APlayerCannotTriggerACharacterToLevelUpWithMorePointsThanAvailable()
        {
            var useCase = new LevelUpCharacterUseCase(StorageProvider);

            var character = await FakeCharacterFactory.CreateAndStoreCharacter();
            character.AddXP(1000);
            await StorageProvider.SaveCharacter(character);

            var e = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await useCase.Handle(new AuthenticatedRequest<LevelUpCharacterUseCaseRequest>(character.Owner, new(new()
                {
                    [CharacterStat.TOUGHNESS] = 10
                })));
            });

            Assert.That(e!.Message, Is.EqualTo(Constants.ErrorMessages.INVALID_STAT_POINTS_AMOUNT));
        }

        [Test]
        public async Task APlayerCanTriggerACharacterLevelUpSelectingTheAttributesToImprove()
        {
            var useCase = new LevelUpCharacterUseCase(StorageProvider);

            var character = await FakeCharacterFactory.CreateAndStoreCharacter();
            character.AddXP(1000);
            await StorageProvider.SaveCharacter(character);

            await useCase.Handle(new AuthenticatedRequest<LevelUpCharacterUseCaseRequest>(character.Owner, new(new()
            {
                [CharacterStat.TOUGHNESS] = 2,
                [CharacterStat.DAMAGE] = 1
            })));

            var updatedCharacter = await StorageProvider.FindCharacterById(character.Id);
            Assert.That(updatedCharacter!.Toughness, Is.EqualTo(character.Toughness + 2));
            Assert.That(updatedCharacter!.Damage, Is.EqualTo(character.Damage + 1));
            Assert.That(updatedCharacter!.PointsToLevelUp, Is.EqualTo(0));
            Assert.That(updatedCharacter!.IsLevelingUp, Is.False);
        }
    }
}