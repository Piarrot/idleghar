using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Characters;

namespace IdlegharDotnetDomain.UseCases.Characters
{
    public class LevelUpCharacterUseCase
    {
        private ICharactersProvider CharactersProvider;

        public LevelUpCharacterUseCase(ICharactersProvider charactersProvider)
        {
            this.CharactersProvider = charactersProvider;
        }

        public async Task Handle(AuthenticatedRequest<LevelUpCharacterUseCaseRequest> authenticatedRequest)
        {
            var character = await CharactersProvider.FindByPlayerId(authenticatedRequest.CurrentPlayerCreds.Id);
            if (character == null) throw new InvalidOperationException(Constants.ErrorMessages.CHARACTER_NOT_CREATED);
            if (!character.IsLevelingUp) throw new InvalidOperationException(Constants.ErrorMessages.CHARACTER_IS_NOT_LEVELING_UP);

            var req = authenticatedRequest.Request;

            var totalAmount = req.attributeIncrease.Sum((kp) => kp.Value);
            if (totalAmount > character.PointsToLevelUp) throw new ArgumentException(Constants.ErrorMessages.INVALID_STAT_POINTS_AMOUNT);

            foreach (var attrIncrease in req.attributeIncrease)
            {
                character.AddAttrPoints(attrIncrease.Key, attrIncrease.Value);
            }

            await this.CharactersProvider.Save(character);
        }
    }
}