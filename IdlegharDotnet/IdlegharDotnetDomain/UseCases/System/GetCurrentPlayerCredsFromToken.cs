using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.UseCases.Players
{
    public class GetCurrentPlayerCredsFromToken
    {
        IPlayersProvider playersProvider;
        IAuthProvider authProvider;

        public GetCurrentPlayerCredsFromToken(IPlayersProvider playersProvider, IAuthProvider authProvider)
        {
            this.playersProvider = playersProvider;
            this.authProvider = authProvider;
        }

        public async Task<PlayerCreds> Handle(string token)
        {
            var email = authProvider.ParseTokenEmail(token);
            if (email == null)
            {
                throw new ArgumentException(Constants.ErrorMessages.INVALID_CREDENTIALS);
            }
            PlayerCreds? playerCreds = await playersProvider.FindCredsFromEmail(email);
            if (playerCreds == null)
            {
                throw new ArgumentException(Constants.ErrorMessages.INVALID_CREDENTIALS);
            }
            return playerCreds;
        }
    }
}