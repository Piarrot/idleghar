using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Auth;

namespace IdlegharDotnetDomain;

public class ValidateEmailUseCase
{
    private IUsersProvider UsersProvider { get; set; }
    public ValidateEmailUseCase(IUsersProvider usersProvider)
    {
        UsersProvider = usersProvider;
    }

    public async Task Handle(ValidateEmailUseCaseRequest request)
    {
        var user = await this.UsersProvider.FindById(request.Id);
        if (user == null)
        {
            throw new ArgumentException("Invalid user Id");
        }

        if (user.EmailValidated)
        {
            throw new ArgumentException("Email already validated");
        }

        if (user.EmailValidationCode != request.Code)
        {
            throw new ArgumentException("Invalid Code");
        }

        user.EmailValidated = true;
        user.EmailValidationCode = null;

        await this.UsersProvider.Save(user);
    }
}