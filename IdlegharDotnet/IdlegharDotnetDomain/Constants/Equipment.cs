using IdlegharDotnetDomain.Entities.Random;
using IdlegharDotnetShared.Constants;

namespace IdlegharDotnetDomain.Constants
{
    public static class Equipment
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
    }
}