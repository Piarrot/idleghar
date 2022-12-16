using IdlegharDotnetShared;

namespace IdlegharDotnetBackend;

public class RegisterUseCase
{
    private IUsersProvider UsersProvider;
    private ICryptoProvider CryptoProvider;
    private IEmailsProvider EmailsProvider;

    public RegisterUseCase(IUsersProvider usersProvider, ICryptoProvider cryptoProvider, IEmailsProvider emailsProvider)
    {
        UsersProvider = usersProvider;
        CryptoProvider = cryptoProvider;
        EmailsProvider = emailsProvider;
    }

    public async Task Handle(RegisterUseCaseInput input)
    {
        var existingUser = await this.UsersProvider.FindUserByEmail(input.Email);

        if (existingUser != null)
        {
            throw new EmailInUseException();
        }

        User newUser = new User()
        {
            Id = Guid.NewGuid().ToString(),
            Email = input.Email,
            Username = input.Username,
            Password = CryptoProvider.HashPassword(input.Password),
        };

        await this.UsersProvider.Save(newUser);
        await this.EmailsProvider.sendEmail(new SendEmailRequest
        {
            To = newUser.Email,
            Template = await this.EmailsProvider.GetTemplate(EmailTemplateNames.VALIDATION_CODE),
            Context = new Dictionary<string, string>
            {
                ["username"] = newUser.Username,
                ["code"] = CryptoProvider.GetRandomNumberDigits(6)
            }
        });
    }
}
