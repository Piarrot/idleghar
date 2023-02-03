using IdlegharDotnetDomain.Entities.Random;

namespace IdlegharDotnetDomain.Constants
{
    public static class Characters
    {
        public static readonly int TOUGHNESS_TO_MAX_HP_MULTIPLIER = 10;

        public enum Stat
        {
            DAMAGE,
            TOUGHNESS,
        }

        public static UniformDistribution<Stat> RandomStat = new()
        {
            Stat.DAMAGE,
            Stat.TOUGHNESS
        };
    }
}