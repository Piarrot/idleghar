using IdlegharDotnetDomain.Entities.Random;
using IdlegharDotnetShared.Constants;

namespace IdlegharDotnetDomain.Constants
{
    public static class EquipmentItems
    {
        public static UniformDistribution<EquipmentType> RandomEquipmentType = new(){
            EquipmentType.Head,
            EquipmentType.Armor,
            EquipmentType.Necklace,
            EquipmentType.Boots,
            EquipmentType.Weapon,
            EquipmentType.Bracer,
            EquipmentType.Ring
        };

        public static Dictionary<ItemQuality, RandomIntRange> RandomAbilityIncrease = new()
        {
            [ItemQuality.Common] = new(1, 1),
            [ItemQuality.Exceptional] = new(2, 4),
            [ItemQuality.Enchanted] = new(5, 7),
            [ItemQuality.Mythic] = new(8, 10),
            [ItemQuality.Legendary] = new(11, 13),
        };
    }
}