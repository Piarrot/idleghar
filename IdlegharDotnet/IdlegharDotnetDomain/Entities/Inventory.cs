using IdlegharDotnetDomain.Entities.Items;
using IdlegharDotnetShared.Constants;

namespace IdlegharDotnetDomain.Entities
{
    [Serializable()]
    public class Inventory : Entity
    {
        public Character Owner { get; set; }

        public Equipment? Weapon
        {
            get
            {
                return this.EquippedItems.Find((e) => e.Type == EquipmentType.Weapon);
            }
        }

        private List<Equipment> EquippedItems { get; set; } = new();

        public int EquippedDamage
        {
            get
            {
                return this.EquippedItems.Sum(e => e.GetStatIncrease(Constants.Characters.Stat.DAMAGE));
            }
        }

        public Inventory(Character owner)
        {
            Owner = owner;
        }

        public Equipment? EquipItem(Equipment equipment)
        {
            var oldEquipmentIndex = this.EquippedItems.FindIndex(e => equipment.Type == e.Type);
            Equipment? oldEquipment = null;
            if (oldEquipmentIndex >= 0)
            {
                oldEquipment = this.EquippedItems[oldEquipmentIndex];
                this.EquippedItems.Remove(oldEquipment);
            }
            this.EquippedItems.Add(equipment);
            return oldEquipment;
        }
    }
}