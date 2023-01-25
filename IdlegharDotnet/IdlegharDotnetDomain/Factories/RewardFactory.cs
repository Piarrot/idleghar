using IdlegharDotnetDomain.Entities.Rewards;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Constants;

namespace IdlegharDotnetDomain.Factories
{
    public class RewardFactory
    {
        public IRandomnessProvider RandomnessProvider;

        public RewardFactory(IRandomnessProvider randomnessProvider)
        {
            RandomnessProvider = randomnessProvider;
        }

        public List<Reward> CreateQuestRewards(Difficulty questDifficulty)
        {
            return new(){
                new XPReward(),
                new EquipmentReward(),
            };
        }
    }
}