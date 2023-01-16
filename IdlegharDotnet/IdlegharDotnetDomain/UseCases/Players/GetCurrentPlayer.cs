using IdlegharDotnetDomain.Transformers;
using IdlegharDotnetShared;

namespace IdlegharDotnetDomain.UseCases.Players
{
    public class GetCurrentPlayer
    {
        public PlayerViewModel Handle(AuthenticatedRequest req)
        {
            PlayerTransformer transformer = new();
            return transformer.TransformOne(req.CurrentPlayer);
        }
    }
}