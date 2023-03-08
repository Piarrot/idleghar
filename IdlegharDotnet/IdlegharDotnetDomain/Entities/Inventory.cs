using IdlegharDotnetDomain.Entities.Items;
using IdlegharDotnetShared.SharedConstants;

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
                return this.EquippedItems.Sum(e => e.GetStatIncrease(CharacterStat.DAMAGE));
            }
        }

        public Inventory(Character owner)
        {
            Owner = owner;
        }

        public Equipment? EquipItem(Equipment newEquipment)
        {
            Equipment? oldEquipment = this.EquippedItems.Find(e => newEquipment.Type == e.Type);
            if (oldEquipment != null)
            {
                this.EquippedItems.Remove(oldEquipment);
            }
            this.EquippedItems.Add(newEquipment);
            return oldEquipment;
        }
    }
}