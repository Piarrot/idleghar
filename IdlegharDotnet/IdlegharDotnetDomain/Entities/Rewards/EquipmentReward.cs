using IdlegharDotnetDomain.Entities.Items;

namespace IdlegharDotnetDomain.Entities.Rewards
{
    [Serializable()]
    public class EquipmentReward : Reward
    {
        public Equipment Equipment { get; }

        public EquipmentReward(Equipment equipment)
        {
            Equipment = equipment;
        }

        public override void Claim(Character character)
        {
            throw new NotImplementedException();
        }
    }
}