using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Characters;

namespace IdlegharDotnetDomain.UseCases.Characters
{
    public class CreateCharacterUseCase
    {
        private IUsersProvider UsersProvider;

        public CreateCharacterUseCase(IUsersProvider usersProvider)
        {
            UsersProvider = usersProvider;
        }

        public async Task<Character> Handle(AuthenticatedRequest<CreateCharacterUseCaseRequest> authRequest)
        {
            if (authRequest.CurrentUser.Character != null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.MORE_THAN_ONE_CHARACTER);
            }

            var newCharacter = new Character
            {
                Name = authRequest.Request.Name
            };

            authRequest.CurrentUser.Character = newCharacter;
            await UsersProvider.Save(authRequest.CurrentUser);


            return newCharacter;
        }
    }
}