using IdlegharDotnetDomain.Constants;

namespace IdlegharDotnetDomain.Entities.Items
{
    [Serializable()]
    public class EquipmentStats
    {
        Dictionary<Constants.Characters.Stat, int> Stats = new();

        public int this[Constants.Characters.Stat stat]
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

        public int GetStat(Constants.Characters.Stat stat)
        {
            int value = 0;
            Stats.TryGetValue(stat, out value);
            return value;
        }

        public void Add(Characters.Stat stat, int value)
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