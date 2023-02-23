using IdlegharDotnetDomain.Tests;
using IdlegharDotnetShared.Characters;
using NUnit.Framework;

namespace IdlegharDotnetDomain.UseCases.Characters.Tests
{
    public class LevelUpCharacterUseCaseTests : BaseTests
    {
        [Test]
        public async Task APlayerCannotTriggerACharacterToLevelUpIfCharacterHasNotEnoughXP()
        {
            var useCase = new LevelUpCharacterUseCase();
            var player = await FakePlayerFactory.CreateAndStorePlayerAndCharacter();
            Assert.Throws<InvalidOperationException>(() =>
            {
                useCase.Handle(new AuthenticatedRequest<LevelUpCharacterUseCaseRequest>(player, new(new()
                {
                    [IdlhegarDotnetShared.Constants.Characters.Stat.TOUGHNESS] = 3
                })));
            });
        }
        [Test]
        public void APlayerCanTriggerACharacterLevelUpSelectingTheAttributesToImprove()
        {
            Assert.Fail();
        }
    }
}