using IdlegharDotnetDomain.Entities.Random;
using IdlegharDotnetShared.SharedConstants;

namespace IdlegharDotnetDomain.Constants
{
    public class Quests
    {
        public static UniformDistribution<Difficulty> RandomDifficulty = new(){
                Difficulty.EASY,
                Difficulty.NORMAL,
                Difficulty.HARD,
                Difficulty.LEGENDARY
            };

        public static readonly int EncountersPerQuest = 7;

        // Specs for quests random count: 
        // https://docs.google.com/document/d/1loOzBcBmZVcGhZWQc69hp5oNjqyMfc-GvOeIxjzq_9M/edit#heading=h.obgpyyfs06mo
        public static readonly Dictionary<Difficulty, RandomValue<int>> CountByDifficulty = new(){
            { Difficulty.EASY, new RandomIntRange(1,3) },
            { Difficulty.NORMAL, new RandomIntRange(2,4) },
            { Difficulty.HARD, new RandomIntRange(1,3) },
            { Difficulty.LEGENDARY, new RandomIntRange(1,1) },
        };

        public static readonly Dictionary<Difficulty, ArbitraryPartialDistribution<ItemQuality>> ItemRewardChances = new()
        {
            [Difficulty.EASY] = new()
            {
                [ItemQuality.Common] = 0.1,
                [ItemQuality.Exceptional] = 0.05,
                [ItemQuality.Enchanted] = 0.01
            },
            [Difficulty.NORMAL] = new()
            {
                [ItemQuality.Common] = 0.2,
                [ItemQuality.Exceptional] = 0.1,
                [ItemQuality.Enchanted] = 0.05,
                [ItemQuality.Mythic] = 0.01,
            },
            [Difficulty.HARD] = new()
            {
                [ItemQuality.Common] = 0.3,
                [ItemQuality.Exceptional] = 0.2,
                [ItemQuality.Enchanted] = 0.1,
                [ItemQuality.Mythic] = 0.05,
            },
            [Difficulty.LEGENDARY] = new()
            {
                [ItemQuality.Common] = 0.2,
                [ItemQuality.Exceptional] = 0.3,
                [ItemQuality.Enchanted] = 0.3,
                [ItemQuality.Mythic] = 0.1,
                [ItemQuality.Legendary] = 0.1
            },
        };
    }
}