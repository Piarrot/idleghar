using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Characters;

namespace IdlegharDotnetDomain.UseCases.Characters
{
    public class CreateCharacterUseCase
    {
        private IStorageProvider StorageProvider;

        public CreateCharacterUseCase(IStorageProvider playersProvider)
        {
            StorageProvider = playersProvider;
        }

        public async Task<Character> Handle(AuthenticatedRequest<CreateCharacterUseCaseRequest> authRequest)
        {
            var player = await StorageProvider.GetPlayerByIdOrThrow(authRequest.CurrentPlayerCreds.Id);
            if (player.Character != null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.MORE_THAN_ONE_CHARACTER);
            }

            var newCharacter = new Character(player!)
            {
                Name = authRequest.Request.Name
            };

            player.Character = newCharacter;
            await StorageProvider.SavePlayer(player);

            return newCharacter;
        }
    }
}