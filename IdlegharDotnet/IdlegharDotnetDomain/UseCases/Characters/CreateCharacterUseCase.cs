using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Characters;

namespace IdlegharDotnetDomain.UseCases.Characters
{
    public class CreateCharacterUseCase
    {
        private ICharactersProvider CharactersProvider;

        public CreateCharacterUseCase(ICharactersProvider charactersProvider)
        {
            CharactersProvider = charactersProvider;
        }

        public async Task<Character> Handle(AuthenticatedRequest<CreateCharacterUseCaseRequest> authRequest)
        {
            var character = await CharactersProvider.FindByPlayerId(authRequest.CurrentPlayer.Id);
            if (character != null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.MORE_THAN_ONE_CHARACTER);
            }

            var newCharacter = new Character(authRequest.CurrentPlayer)
            {
                Name = authRequest.Request.Name
            };

            await CharactersProvider.Save(newCharacter);


            return newCharacter;
        }
    }
}