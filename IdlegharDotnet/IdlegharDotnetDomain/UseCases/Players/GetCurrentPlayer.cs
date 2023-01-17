using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetDomain.Transformers;
using IdlegharDotnetShared;

namespace IdlegharDotnetDomain.UseCases.Players
{
    public class GetCurrentPlayersInventory
    {
        IPlayersProvider PlayersProvider;

        public GetCurrentPlayersInventory(IPlayersProvider playersProvider)
        {
            PlayersProvider = playersProvider;
        }

        public async Task<PlayerViewModel> Handle(AuthenticatedRequest req)
        {
            PlayerTransformer transformer = new();
            Player player = await PlayersProvider.GetByIdOrThrow(req.CurrentPlayerCreds.Id);

            return transformer.TransformOne(player!);
        }
    }
}