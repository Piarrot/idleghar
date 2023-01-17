using IdlegharDotnetDomain.Entities.Rewards;
using IdlegharDotnetShared.Rewards;

namespace IdlegharDotnetDomain.Transformers
{
    public class RewardTransformer : Transformer<Reward, RewardViewModel>
    {
        public override RewardViewModel TransformOne(Reward entity)
        {
            return new RewardViewModel()
            {
                Id = entity.Id,
            };
        }
    }
}