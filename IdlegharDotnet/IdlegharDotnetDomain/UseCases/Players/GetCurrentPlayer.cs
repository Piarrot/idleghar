using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetDomain.Transformers;
using IdlegharDotnetShared;

namespace IdlegharDotnetDomain.UseCases.Players
{
    public class GetCurrentPlayer
    {
        IStorageProvider StorageProvider;

        public GetCurrentPlayer(IStorageProvider playersProvider)
        {
            StorageProvider = playersProvider;
        }

        public async Task<PlayerViewModel> Handle(AuthenticatedRequest req)
        {
            PlayerTransformer transformer = new();
            Player player = await StorageProvider.GetPlayerByIdOrThrow(req.CurrentPlayerCreds.Id);

            return transformer.TransformOne(player);
        }
    }
}