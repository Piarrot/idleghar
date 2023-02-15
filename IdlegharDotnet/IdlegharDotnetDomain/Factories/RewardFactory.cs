using IdlegharDotnetDomain.Entities.Items;
using IdlegharDotnetDomain.Entities.Random;
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

        public Reward CreateQuestRewards(Difficulty questDifficulty)
        {
            Reward reward = new();
            reward.AddXP(20);

            Optional<ItemQuality> optionalItemQuality = this.RandomnessProvider.GetRandomItemQualityQuestRewardFromDifficulty(questDifficulty);
            if (optionalItemQuality.HasValue)
            {

                reward.AddItem(CreateEquipmentReward(optionalItemQuality.Value));
            }

            return reward;
        }

        public Equipment CreateEquipmentReward(ItemQuality quality)
        {
            var eqFactory = new EquipmentFactory(this.RandomnessProvider);
            return eqFactory.CreateEquipment(quality);
        }
    }
}