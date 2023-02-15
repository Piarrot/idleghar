using IdlegharDotnetShared.Constants;

namespace IdlegharDotnetDomain.Entities.Items
{
    [Serializable()]
    public class Equipment : Item
    {
        public EquipmentType Type { get; set; }
        public EquipmentStats StatChanges { get; set; } = new();
        public int GetStatIncrease(Constants.Characters.Stat stat)
        {
            return StatChanges.GetStat(stat);
        }

        public void AddStat(Constants.Characters.Stat stat, int value)
        {
            StatChanges.Add(stat, value);
        }
    }
}