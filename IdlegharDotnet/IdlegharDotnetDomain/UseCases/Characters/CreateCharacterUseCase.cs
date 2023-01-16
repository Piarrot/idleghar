using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Characters;

namespace IdlegharDotnetDomain.UseCases.Characters
{
    public class CreateCharacterUseCase
    {
        private IPlayersProvider PlayersProvider;

        public CreateCharacterUseCase(IPlayersProvider playersProvider)
        {
            PlayersProvider = playersProvider;
        }

        public async Task<Character> Handle(AuthenticatedRequest<CreateCharacterUseCaseRequest> authRequest)
        {
            if (authRequest.CurrentPlayer.Character != null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.MORE_THAN_ONE_CHARACTER);
            }

            var newCharacter = new Character
            {
                Name = authRequest.Request.Name
            };

            authRequest.CurrentPlayer.Character = newCharacter;
            await PlayersProvider.Save(authRequest.CurrentPlayer);


            return newCharacter;
        }
    }
}