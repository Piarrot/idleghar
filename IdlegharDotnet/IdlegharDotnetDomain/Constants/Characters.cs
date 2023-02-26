using IdlegharDotnetDomain.Entities.Random;
using IdlegharDotnetShared.SharedConstants;

namespace IdlegharDotnetDomain.Constants
{
    public static class Characters
    {
        public static readonly int TOUGHNESS_TO_MAX_HP_MULTIPLIER = 10;

        public static UniformDistribution<CharacterStat> RandomStat = new()
        {
            CharacterStat.DAMAGE,
            CharacterStat.TOUGHNESS
        };
    }
}