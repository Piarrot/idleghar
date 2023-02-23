using IdlegharDotnetDomain.Entities.Random;
using IdlegharDotnetShared.Constants;

namespace IdlegharDotnetDomain.Constants
{
    public class Encounters
    {

        //Spec: https://docs.google.com/document/d/1loOzBcBmZVcGhZWQc69hp5oNjqyMfc-GvOeIxjzq_9M/edit#heading=h.sfzikvm2c1lz

        public static readonly Dictionary<Difficulty, ArbitraryDistribution<Difficulty>> DifficultyByQuestDifficulty = new()
        {

            [Difficulty.EASY] =
                new()
                {
                    [Difficulty.EASY] = 0.6,
                    [Difficulty.NORMAL] = 0.3,
                    [Difficulty.HARD] = 0.1,
                },
            [Difficulty.NORMAL] =
                new()
                {
                    [Difficulty.EASY] = 0.2,
                    [Difficulty.NORMAL] = 0.6,
                    [Difficulty.HARD] = 0.2,
                },
            [Difficulty.HARD] =
                new()
                {
                    [Difficulty.NORMAL] = 0.39,
                    [Difficulty.HARD] = 0.6,
                    [Difficulty.LEGENDARY] = 0.01,
                },
            [Difficulty.LEGENDARY] =
                new()
                {
                    [Difficulty.HARD] = 0.6,
                    [Difficulty.LEGENDARY] = 0.4,
                },
        };

        public static readonly Dictionary<Difficulty, RandomIntRange> EnemyHPByDifficulty = new()
        {
            [Difficulty.EASY] = new(4, 8),
            [Difficulty.NORMAL] = new(10, 18),
            [Difficulty.HARD] = new(20, 38),
            [Difficulty.LEGENDARY] = new(40, 150),
        };

        public static readonly Func<int, int> CombatXPByEncounterHP = (hp) => hp * 10;

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