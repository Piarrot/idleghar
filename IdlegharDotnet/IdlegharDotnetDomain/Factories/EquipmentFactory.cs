
using IdlegharDotnetDomain.Constants;
using IdlegharDotnetDomain.Entities.Items;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Constants;

namespace IdlegharDotnetDomain.Factories
{
    public class EquipmentFactory
    {
        public EquipmentFactory(IRandomnessProvider randomnessProvider)
        {
            RandomnessProvider = randomnessProvider;
        }

        public IRandomnessProvider RandomnessProvider { get; }

        public Equipment CreateEquipment(ItemQuality itemQuality)
        {
            var itemType = EquipmentItems.RandomEquipmentType.ResolveOne(this.RandomnessProvider);

            Equipment eq = this.CreateEquipment(itemQuality, itemType);

            return eq;
        }

        public Equipment CreateEquipment(ItemQuality itemQuality, EquipmentType itemType)
        {
            return new Equipment()
            {
                Quality = itemQuality,
                Type = itemType,
                StatChanges = GetEquipmentStats(itemQuality)
            };
        }

        public EquipmentStats GetEquipmentStats(ItemQuality quality)
        {
            var remainingStatChange = EquipmentItems.RandomAbilityIncrease[quality].ResolveOne(this.RandomnessProvider);

            EquipmentStats stats = new();

            while (remainingStatChange > 0)
            {
                int value = this.RandomnessProvider.GetRandomInt(1, remainingStatChange);
                stats.Add(Characters.RandomStat.ResolveOne(this.RandomnessProvider), value);
                remainingStatChange -= value;
            }

            return stats;
        }
    }
}