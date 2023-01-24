using IdlegharDotnetShared.Constants;

namespace IdlegharDotnetDomain.Entities.Items
{
    [Serializable()]
    public class Weapon : Equipment
    {
        public int DamageIncrease { get; set; }

        public Weapon()
        {
            this.Type = EquipmentType.Weapon;
        }

        public override Equipment? EquipTo(Inventory inventory)
        {
            return inventory.EquipWeapon(this);
        }
    }
}