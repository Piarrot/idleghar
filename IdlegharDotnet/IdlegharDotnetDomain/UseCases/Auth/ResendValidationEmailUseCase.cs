using IdlegharDotnetDomain.Constants;
using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Auth;

namespace IdlegharDotnetDomain.UseCases.Auth
{
    public class ResendValidationEmailUseCase
    {
        private IUsersProvider UsersProvider;
        private IEmailsProvider EmailsProvider;
        private ICryptoProvider CryptoProvider;

        public ResendValidationEmailUseCase(IUsersProvider usersProvider, IEmailsProvider emailsProvider, ICryptoProvider cryptoProvider)
        {
            UsersProvider = usersProvider;
            EmailsProvider = emailsProvider;
            CryptoProvider = cryptoProvider;
        }

        public async Task Handle(ResendValidationUseCaseRequest req)
        {
            var user = await UsersProvider.FindByEmail(req.Email);

            if (user == null)
            {
                throw new ArgumentException(Constants.ErrorMessages.INVALID_EMAIL);
            }

            if (user.EmailValidated)
            {
                throw new ArgumentException(Constants.ErrorMessages.EMAIL_ALREADY_VALIDATED);
            }

            var code = CryptoProvider.GetRandomNumberDigits(6);

            user.EmailValidationCode = code;

            await UsersProvider.Save(user);
            await EmailsProvider.sendEmail(new SendEmailRequest(
                user.Email,
                await EmailsProvider.GetTemplate(EmailTemplateNames.VALIDATION_CODE),
                new Dictionary<string, string>
                {
                    ["username"] = user.Username,
                    ["code"] = code
                }
            ));
        }
    }
}