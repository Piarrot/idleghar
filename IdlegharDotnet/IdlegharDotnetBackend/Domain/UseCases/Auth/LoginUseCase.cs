using IdlegharDotnetShared;

namespace IdlegharDotnetBackend;
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

    public async Task<LoginUseCaseOutput> Handle(LoginUseCaseInput input)
    {
        var user = await UsersProvider.FindUserByEmail(input.EmailOrUsername) ?? await UsersProvider.FindUserByUsername(input.EmailOrUsername);
        if (user == null || !CryptoProvider.DoesPasswordMatches(user.Password, input.Password))
        {
            throw new WrongCredentialsException();
        }

        var token = AuthProvider.GenerateToken(user);
        return new LoginUseCaseOutput()
        {
            Token = token,
        };
    }
}