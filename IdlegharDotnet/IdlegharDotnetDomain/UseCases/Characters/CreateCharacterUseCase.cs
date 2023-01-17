using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Characters;

namespace IdlegharDotnetDomain.UseCases.Characters
{
    public class CreateCharacterUseCase
    {
        private ICharactersProvider CharactersProvider;
        private IPlayersProvider PlayersProvider;

        public CreateCharacterUseCase(ICharactersProvider charactersProvider, IPlayersProvider playersProvider)
        {
            CharactersProvider = charactersProvider;
            PlayersProvider = playersProvider;
        }

        public async Task<Character> Handle(AuthenticatedRequest<CreateCharacterUseCaseRequest> authRequest)
        {
            var character = await CharactersProvider.FindByPlayerId(authRequest.CurrentPlayerCreds.Id);
            if (character != null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.MORE_THAN_ONE_CHARACTER);
            }
            var player = await PlayersProvider.FindById(authRequest.CurrentPlayerCreds.Id);

            var newCharacter = new Character(player!)
            {
                Name = authRequest.Request.Name
            };

            await CharactersProvider.Save(newCharacter);


            return newCharacter;
        }
    }
}