using IdlegharDotnetDomain.Entities.Random;
using IdlegharDotnetShared.Constants;

namespace IdlegharDotnetDomain.Constants
{
    public class Encounters
    {

        //Spec: https://docs.google.com/document/d/1loOzBcBmZVcGhZWQc69hp5oNjqyMfc-GvOeIxjzq_9M/edit#heading=h.sfzikvm2c1lz

        public static readonly Dictionary<Difficulty, ArbitraryDistribution<Difficulty>> EncounterDifficultyByQuestDifficulty = new()
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

        public static readonly Dictionary<Difficulty, int> EnemyHPByDifficulty = new()
        {
            [Difficulty.EASY] = 4,
            [Difficulty.NORMAL] = 10,
            [Difficulty.HARD] = 20,
            [Difficulty.LEGENDARY] = 40,
        };
    }
}