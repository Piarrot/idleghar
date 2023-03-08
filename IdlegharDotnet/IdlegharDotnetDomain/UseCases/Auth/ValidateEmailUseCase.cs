using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Auth;

namespace IdlegharDotnetDomain;

public class ValidateEmailUseCase
{
    private IStorageProvider StorageProvider { get; set; }
    public ValidateEmailUseCase(IStorageProvider playersProvider)
    {
        StorageProvider = playersProvider;
    }

    public async Task Handle(ValidateEmailUseCaseRequest request)
    {
        var player = await this.StorageProvider.GetPlayerByIdOrThrow(request.Id);

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

        await this.StorageProvider.SavePlayer(player);
    }
}