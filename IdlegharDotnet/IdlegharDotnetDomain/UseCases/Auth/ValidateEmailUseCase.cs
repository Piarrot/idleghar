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
            throw new ArgumentException(Constants.ErrorMessages.INVALID_USER);
        }

        if (user.EmailValidated)
        {
            throw new ArgumentException(Constants.ErrorMessages.EMAIL_ALREADY_VALIDATED);
        }

        if (user.EmailValidationCode != request.Code)
        {
            throw new ArgumentException(Constants.ErrorMessages.INVALID_VALIDATION_CODE);
        }

        user.EmailValidated = true;
        user.EmailValidationCode = null;

        await this.UsersProvider.Save(user);
    }
}