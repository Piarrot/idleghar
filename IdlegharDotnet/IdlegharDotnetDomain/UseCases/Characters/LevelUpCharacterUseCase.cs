using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Characters;

namespace IdlegharDotnetDomain.UseCases.Characters
{
    public class LevelUpCharacterUseCase
    {
        public LevelUpCharacterUseCase(IStorageProvider playersProvider)
        {
            StorageProvider = playersProvider;
        }

        public IStorageProvider StorageProvider { get; }

        public async Task Handle(AuthenticatedRequest<LevelUpCharacterUseCaseRequest> authenticatedRequest)
        {
            var character = await StorageProvider.GetCharacterByPlayerIdOrThrow(authenticatedRequest.CurrentPlayerCreds.Id);
            if (!character.IsLevelingUp) throw new InvalidOperationException(Constants.ErrorMessages.CHARACTER_IS_NOT_LEVELING_UP);

            var req = authenticatedRequest.Request;

            var totalAmount = req.attributeIncrease.Sum((kp) => kp.Value);
            if (totalAmount > character.PointsToLevelUp) throw new ArgumentException(Constants.ErrorMessages.INVALID_STAT_POINTS_AMOUNT);

            foreach (var attrIncrease in req.attributeIncrease)
            {
                character.AddAttrPoints(attrIncrease.Key, attrIncrease.Value);
            }

            await this.StorageProvider.SaveCharacter(character);
        }
    }
}