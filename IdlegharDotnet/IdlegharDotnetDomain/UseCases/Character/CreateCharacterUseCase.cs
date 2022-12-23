using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Exceptions;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Character;

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
                throw new MoreThanOneCharacterException();
            }

            var newCharacter = new Character
            {
                Id = Guid.NewGuid().ToString(),
                Name = authRequest.Request.Name
            };

            authRequest.CurrentUser.Character = newCharacter;
            await UsersProvider.Save(authRequest.CurrentUser);


            return newCharacter;
        }
    }
}