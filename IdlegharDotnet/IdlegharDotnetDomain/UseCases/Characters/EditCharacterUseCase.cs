using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Characters;

namespace IdlegharDotnetDomain.UseCases.Characters
{
    public class EditCharacterUseCase
    {
        public EditCharacterUseCase(IStorageProvider playersProvider)
        {
            StorageProvider = playersProvider;
        }

        public IStorageProvider StorageProvider { get; }

        public async Task<Character> Handle(AuthenticatedRequest<EditCharacterUseCaseRequest> authenticatedRequest)
        {
            var character = await StorageProvider.GetCharacterByPlayerIdOrThrow(authenticatedRequest.CurrentPlayerCreds.Id);

            character.Name = authenticatedRequest.Request.Name;

            await StorageProvider.SavePlayer(character.Owner);

            return character;
        }

    }
}