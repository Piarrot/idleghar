using IdlegharDotnetDomain.Constants;
using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Entities.Notifications;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Auth;

namespace IdlegharDotnetDomain.UseCases.Auth
{
    public class RegisterUseCase
    {
        private IPlayersProvider PlayersProvider;
        private ICryptoProvider CryptoProvider;
        private IEmailsProvider EmailsProvider;

        public RegisterUseCase(IPlayersProvider playersProvider, ICryptoProvider cryptoProvider, IEmailsProvider emailsProvider)
        {
            PlayersProvider = playersProvider;
            CryptoProvider = cryptoProvider;
            EmailsProvider = emailsProvider;
        }

        public async Task<RegisterUseCaseResponse> Handle(RegisterUseCaseRequest req)
        {
            var existingPlayer = await PlayersProvider.FindByEmail(req.Email);

            if (existingPlayer != null)
            {
                throw new ArgumentException(Constants.ErrorMessages.EMAIL_IN_USE);
            }

            var code = CryptoProvider.GetRandomNumberDigits(6);

            Player newPlayer = new Player()
            {
                Email = req.Email,
                Username = req.Username,
                Password = CryptoProvider.HashPassword(req.Password),
                EmailValidated = false,
                EmailValidationCode = code
            };

            await PlayersProvider.Save(newPlayer);
            await EmailsProvider.SendEmail(new SendEmailRequest(
                newPlayer.Email,
                EmailTemplateNames.VALIDATION_CODE,
                new Dictionary<string, string>
                {
                    ["username"] = newPlayer.Username,
                    ["code"] = code
                }
            ));

            return new RegisterUseCaseResponse(newPlayer.Id, newPlayer.Email, newPlayer.Username);
        }
    }
}