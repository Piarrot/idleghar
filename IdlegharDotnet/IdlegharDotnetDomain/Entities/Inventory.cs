using IdlegharDotnetDomain.Entities.Items;

namespace IdlegharDotnetDomain.Entities
{
    [Serializable()]
    public class Inventory : Entity
    {
        public Character Owner { get; set; }

        public Weapon? Weapon { get; private set; } = null;
        public int EquippedDamage
        {
            get
            {
                return Weapon?.DamageIncrease ?? 0;
            }
        }

        public Inventory(Character owner)
        {
            Owner = owner;
        }

        public Equipment? EquipWeapon(Weapon weapon)
        {
            Equipment? oldWeapon = this.Weapon;
            this.Weapon = weapon;
            return oldWeapon;
        }

        public Equipment? EquipItem(Equipment equipment)
        {
            return equipment.EquipTo(this);
        }
    }
}