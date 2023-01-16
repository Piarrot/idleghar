using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Characters;

namespace IdlegharDotnetDomain.UseCases.Characters
{
    public class EditCharacterUseCase
    {
        private IPlayersProvider PlayersProvider;

        public EditCharacterUseCase(IPlayersProvider playersProvider)
        {
            PlayersProvider = playersProvider;
        }

        public async Task<Character> Handle(AuthenticatedRequest<EditCharacterUseCaseRequest> authenticatedRequest)
        {
            var character = authenticatedRequest.CurrentPlayer.Character;

            if (character == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.CHARACTER_NOT_CREATED);
            }

            character.Name = authenticatedRequest.Request.Name;

            await PlayersProvider.Save(authenticatedRequest.CurrentPlayer);

            return character;
        }

    }
}