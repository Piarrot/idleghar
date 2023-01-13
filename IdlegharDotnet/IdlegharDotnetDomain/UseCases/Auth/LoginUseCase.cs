using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Auth;

namespace IdlegharDotnetDomain.UseCases.Auth
{
    public class LoginUseCase
    {
        private IAuthProvider AuthProvider { get; set; }
        private IUsersProvider UsersProvider { get; set; }
        private ICryptoProvider CryptoProvider { get; set; }

        public LoginUseCase(IAuthProvider authProvider, IUsersProvider usersProvider, ICryptoProvider cryptoProvider)
        {
            AuthProvider = authProvider;
            UsersProvider = usersProvider;
            CryptoProvider = cryptoProvider;
        }

        public async Task<LoginUseCaseResponse> Handle(LoginUseCaseRequest input)
        {
            var user = await UsersProvider.FindByEmail(input.EmailOrUsername) ?? await UsersProvider.FindByUsername(input.EmailOrUsername);
            if (user == null || !CryptoProvider.DoesPasswordMatches(user.Password, input.Password))
            {
                throw new ArgumentException(Constants.ErrorMessages.INVALID_CREDENTIALS);
            }

            var token = AuthProvider.GenerateToken(user);
            return new LoginUseCaseResponse(token);
        }
    }
}