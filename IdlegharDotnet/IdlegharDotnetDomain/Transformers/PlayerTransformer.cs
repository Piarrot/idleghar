using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared;

namespace IdlegharDotnetDomain.Transformers
{
    public class PlayerTransformer : Transformer<Player, PlayerViewModel>
    {
        ItemTransformer itemTransformer = new ItemTransformer();
        RewardTransformer rewardTransformer = new RewardTransformer();
        CharacterTransformer characterTransformer = new CharacterTransformer();
        public override PlayerViewModel TransformOne(Player entity)
        {
            return new PlayerViewModel()
            {
                Id = entity.Id,
                Email = entity.Email,
                Username = entity.Username,
                Currency = entity.Currency,
                Items = itemTransformer.TransformMany(entity.Items),
                UnclaimedRewards = rewardTransformer.TransformMany(entity.UnclaimedRewards),
                Character = characterTransformer.TransformOneOptional(entity.Character),
            };
        }
    }
}