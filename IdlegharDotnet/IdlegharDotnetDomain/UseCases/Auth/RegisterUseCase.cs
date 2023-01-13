using IdlegharDotnetDomain.Constants;
using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Auth;

namespace IdlegharDotnetDomain.UseCases.Auth
{
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

        public async Task<RegisterUseCaseResponse> Handle(RegisterUseCaseRequest req)
        {
            var existingUser = await UsersProvider.FindByEmail(req.Email);

            if (existingUser != null)
            {
                throw new ArgumentException(Constants.ErrorMessages.EMAIL_IN_USE);
            }

            var code = CryptoProvider.GetRandomNumberDigits(6);

            User newUser = new User()
            {
                Email = req.Email,
                Username = req.Username,
                Password = CryptoProvider.HashPassword(req.Password),
                EmailValidated = false,
                EmailValidationCode = code
            };

            await UsersProvider.Save(newUser);
            await EmailsProvider.sendEmail(new SendEmailRequest(
                newUser.Email,
                await EmailsProvider.GetTemplate(EmailTemplateNames.VALIDATION_CODE),
                new Dictionary<string, string>
                {
                    ["username"] = newUser.Username,
                    ["code"] = code
                }
            ));

            return new RegisterUseCaseResponse(newUser.Id, newUser.Email, newUser.Username);
        }
    }
}