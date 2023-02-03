
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

        public Equipment CreateEquipment(IdlegharDotnetShared.Constants.ItemQuality itemQuality)
        {
            var itemType = EquipmentItems.RandomEquipmentType.ResolveOne(this.RandomnessProvider);

            Equipment eq = this.CreateEquipment(itemQuality, itemType);

            return eq;
        }

        private Equipment CreateEquipment(ItemQuality itemQuality, EquipmentType itemType)
        {
            switch (itemType)
            {
                case EquipmentType.Weapon: return CreateWeapon(itemQuality);
                default:
                    throw new ArgumentException(ErrorMessages.INVALID_ITEM_TYPE);
            }
        }

        private Equipment CreateWeapon(ItemQuality quality)
        {
            return new Weapon()
            {
                Quality = quality,
                DamageIncrease = GetAbilityIncrease(quality)
            };
        }

        private int GetAbilityIncrease(ItemQuality quality)
        {
            return EquipmentItems.RandomAbilityIncrease[quality].ResolveOne(this.RandomnessProvider);
        }
    }
}