using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Auth;

namespace IdlegharDotnetDomain;

public class ValidateEmailUseCase
{
    private IPlayersProvider PlayersProvider { get; set; }
    public ValidateEmailUseCase(IPlayersProvider playersProvider)
    {
        PlayersProvider = playersProvider;
    }

    public async Task Handle(ValidateEmailUseCaseRequest request)
    {
        var player = await this.PlayersProvider.FindById(request.Id);
        if (player == null)
        {
            throw new ArgumentException(Constants.ErrorMessages.INVALID_PLAYER);
        }

        if (player.EmailValidated)
        {
            throw new ArgumentException(Constants.ErrorMessages.EMAIL_ALREADY_VALIDATED);
        }

        if (player.EmailValidationCode != request.Code)
        {
            throw new ArgumentException(Constants.ErrorMessages.INVALID_VALIDATION_CODE);
        }

        player.EmailValidated = true;
        player.EmailValidationCode = null;

        await this.PlayersProvider.Save(player);
    }
}