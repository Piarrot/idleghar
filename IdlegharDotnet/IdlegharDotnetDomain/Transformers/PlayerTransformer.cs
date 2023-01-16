using IdlegharDotnetDomain.Entities;
using IdlegharDotnetShared;

namespace IdlegharDotnetDomain.Transformers
{
    public class PlayerTransformer : Transformer<Player, PlayerViewModel>
    {
        public override PlayerViewModel TransformOne(Player entity)
        {
            return new PlayerViewModel()
            {
                Id = entity.Id,
                Email = entity.Email,
                Username = entity.Username,
                Currency = entity.Currency,
            };
        }
    }
}