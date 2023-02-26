using IdlegharDotnetDomain.Constants;
using IdlegharDotnetShared.SharedConstants;

namespace IdlegharDotnetDomain.Entities.Items
{
    [Serializable()]
    public class EquipmentStats
    {
        Dictionary<CharacterStat, int> Stats = new();

        public int this[CharacterStat stat]
        {
            get
            {
                return GetStat(stat);
            }
            set
            {
                Stats[stat] = value;
            }
        }

        public int GetStat(CharacterStat stat)
        {
            int value = 0;
            Stats.TryGetValue(stat, out value);
            return value;
        }

        public void Add(CharacterStat stat, int value)
        {
            if (Stats.ContainsKey(stat))
            {
                Stats[stat] += value;
            }
            else
            {
                Stats[stat] = value;
            }
        }
    }
}