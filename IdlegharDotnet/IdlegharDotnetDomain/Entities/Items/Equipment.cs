using IdlegharDotnetShared.Constants;

namespace IdlegharDotnetDomain.Entities.Items
{
    [Serializable()]
    public abstract class Equipment : Item
    {
        public EquipmentType Type { get; set; }

        public abstract Equipment? EquipTo(Inventory inventory);
    }
}