using IdlegharDotnetDomain.Constants;
using IdlegharDotnetDomain.Entities.Notifications;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Auth;

namespace IdlegharDotnetDomain.UseCases.Auth
{
    public class ResendValidationEmailUseCase
    {
        private IPlayersProvider PlayersProvider;
        private IEmailsProvider EmailsProvider;
        private ICryptoProvider CryptoProvider;

        public ResendValidationEmailUseCase(IPlayersProvider playersProvider, IEmailsProvider emailsProvider, ICryptoProvider cryptoProvider)
        {
            PlayersProvider = playersProvider;
            EmailsProvider = emailsProvider;
            CryptoProvider = cryptoProvider;
        }

        public async Task Handle(ResendValidationUseCaseRequest req)
        {
            var player = await PlayersProvider.FindByEmail(req.Email);

            if (player == null)
            {
                throw new ArgumentException(Constants.ErrorMessages.INVALID_EMAIL);
            }

            if (player.EmailValidated)
            {
                throw new ArgumentException(Constants.ErrorMessages.EMAIL_ALREADY_VALIDATED);
            }

            var code = CryptoProvider.GetRandomNumberDigits(6);

            player.EmailValidationCode = code;

            await PlayersProvider.Save(player);
            await EmailsProvider.sendEmail(new SendEmailRequest(
                player.Email,
                EmailTemplateNames.VALIDATION_CODE,
                new Dictionary<string, string>
                {
                    ["username"] = player.Username,
                    ["code"] = code
                }
            ));
        }
    }
}