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
            List<Reward> rewards = new();
            rewards.Add(new XPReward());

            var optionalItemQuality = Constants.Quests.QuestItemRewardChances[questDifficulty].ResolveOne(this.RandomnessProvider);
            if (optionalItemQuality.HasValue)
            {
                var eqFactory = new EquipmentFactory(this.RandomnessProvider);
                rewards.Add(new EquipmentReward(eqFactory.CreateEquipment(optionalItemQuality.Value)));
            }

            return rewards;
        }
    }
}