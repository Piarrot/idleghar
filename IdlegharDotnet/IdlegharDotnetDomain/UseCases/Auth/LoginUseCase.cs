using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Auth;

namespace IdlegharDotnetDomain.UseCases.Auth
{
    public class LoginUseCase
    {
        private IAuthProvider AuthProvider { get; set; }
        private IPlayersProvider PlayersProvider { get; set; }
        private ICryptoProvider CryptoProvider { get; set; }

        public LoginUseCase(IAuthProvider authProvider, IPlayersProvider playersProvider, ICryptoProvider cryptoProvider)
        {
            AuthProvider = authProvider;
            PlayersProvider = playersProvider;
            CryptoProvider = cryptoProvider;
        }

        public async Task<LoginUseCaseResponse> Handle(LoginUseCaseRequest input)
        {
            var player = await PlayersProvider.FindByEmail(input.EmailOrUsername) ?? await PlayersProvider.FindByUsername(input.EmailOrUsername);
            if (player == null || !CryptoProvider.DoesPasswordMatches(player.Password, input.Password))
            {
                throw new ArgumentException(Constants.ErrorMessages.INVALID_CREDENTIALS);
            }

            var token = AuthProvider.GenerateToken(player);
            return new LoginUseCaseResponse(token);
        }
    }
}