using IdlegharDotnetShared;

namespace IdlegharDotnetBackend;
public class LoginUseCase
{
    public LoginUseCase()
    {

    }

    public async Task<LoginUseCaseOutput> Handle(LoginUseCaseInput input)
    {
        return new LoginUseCaseOutput();
    }
}