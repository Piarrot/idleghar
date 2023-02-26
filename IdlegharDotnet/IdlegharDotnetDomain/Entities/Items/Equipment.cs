using IdlegharDotnetShared.SharedConstants;

namespace IdlegharDotnetDomain.Entities.Items
{
    [Serializable()]
    public class Equipment : Item
    {
        public EquipmentType Type { get; set; }
        public EquipmentStats StatChanges { get; set; } = new();
        public int GetStatIncrease(CharacterStat stat)
        {
            return StatChanges.GetStat(stat);
        }

        public void AddStat(CharacterStat stat, int value)
        {
            StatChanges.Add(stat, value);
        }
    }
}