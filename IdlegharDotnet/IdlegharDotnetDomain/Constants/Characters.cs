using IdlegharDotnetDomain.Entities.Random;
using static IdlegharDotnetShared.Constants.Characters;

namespace IdlegharDotnetDomain.Constants
{
    public static class Characters
    {
        public static readonly int TOUGHNESS_TO_MAX_HP_MULTIPLIER = 10;



        public static UniformDistribution<Stat> RandomStat = new()
        {
            Stat.DAMAGE,
            Stat.TOUGHNESS
        };
    }
}