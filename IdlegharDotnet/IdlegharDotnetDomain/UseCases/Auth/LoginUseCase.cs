using IdlegharDotnetDomain.Providers;
using IdlegharDotnetDomain.Transformers;
using IdlegharDotnetShared.Auth;

namespace IdlegharDotnetDomain.UseCases.Auth
{
    public class LoginUseCase
    {
        private IAuthProvider AuthProvider { get; set; }
        private IStorageProvider StorageProvider { get; set; }
        private ICryptoProvider CryptoProvider { get; set; }

        public LoginUseCase(IAuthProvider authProvider, IStorageProvider playersProvider, ICryptoProvider cryptoProvider)
        {
            AuthProvider = authProvider;
            StorageProvider = playersProvider;
            CryptoProvider = cryptoProvider;
        }

        public async Task<LoginUseCaseResponse> Handle(LoginUseCaseRequest input)
        {
            var player = await StorageProvider.FindPlayerByEmailOrUsername(input.EmailOrUsername);
            if (player == null || !CryptoProvider.DoesPasswordMatches(player.Password, input.Password))
            {
                throw new ArgumentException(Constants.ErrorMessages.INVALID_CREDENTIALS);
            }

            var token = AuthProvider.GenerateToken(player);
            var transformer = new PlayerTransformer();
            return new LoginUseCaseResponse(token, transformer.TransformOne(player));
        }
    }
}