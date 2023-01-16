using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Characters;

namespace IdlegharDotnetDomain.UseCases.Characters
{
    public class EditCharacterUseCase
    {
        private ICharactersProvider CharactersProvider;

        public EditCharacterUseCase(ICharactersProvider charactersProvider)
        {
            CharactersProvider = charactersProvider;
        }

        public async Task<Character> Handle(AuthenticatedRequest<EditCharacterUseCaseRequest> authenticatedRequest)
        {
            var character = await CharactersProvider.GetCharacterFromPlayerOrThrow(authenticatedRequest.CurrentPlayer);

            character.Name = authenticatedRequest.Request.Name;

            await CharactersProvider.Save(character);

            return character;
        }

    }
}