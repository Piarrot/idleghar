using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Characters;

namespace IdlegharDotnetDomain.UseCases.Characters
{
    public class EditCharacterUseCase
    {
        private IUsersProvider UsersProvider;

        public EditCharacterUseCase(IUsersProvider usersProvider)
        {
            UsersProvider = usersProvider;
        }

        public async Task<Character> Handle(AuthenticatedRequest<EditCharacterUseCaseRequest> authenticatedRequest)
        {
            var character = authenticatedRequest.CurrentUser.Character;

            if (character == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.CHARACTER_NOT_CREATED);
            }

            character.Name = authenticatedRequest.Request.Name;

            await UsersProvider.Save(authenticatedRequest.CurrentUser);

            return character;
        }

    }
}