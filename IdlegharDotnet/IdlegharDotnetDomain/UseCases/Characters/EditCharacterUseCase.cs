using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Character;

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
                throw new InvalidOperationException(Constants.ErrorMessages.EDIT_CHARACTER_NOT_CREATED);
            }

            character.Name = authenticatedRequest.Request.Name;

            await UsersProvider.Save(authenticatedRequest.CurrentUser);

            return character;
        }

    }
}