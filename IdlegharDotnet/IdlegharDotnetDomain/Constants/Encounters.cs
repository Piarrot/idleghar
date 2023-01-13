using IdlegharDotnetDomain.Entities.Random;

namespace IdlegharDotnetDomain.Constants
{
    public class Encounters
    {

        //Spec: https://docs.google.com/document/d/1loOzBcBmZVcGhZWQc69hp5oNjqyMfc-GvOeIxjzq_9M/edit#heading=h.sfzikvm2c1lz

        public static readonly Dictionary<Difficulty, RandomValueFromChances<Difficulty>> EncounterDifficultyByQuestDifficulty = new()
        {
            {
                Difficulty.EASY,
                new(new(){
                    new (Difficulty.EASY,0.6),
                    new (Difficulty.NORMAL,0.3),
                    new (Difficulty.HARD,0.1),
                })
            },
            {
                Difficulty.NORMAL,
                new(new(){
                    new (Difficulty.EASY,0.2),
                    new (Difficulty.NORMAL,0.6),
                    new (Difficulty.HARD,0.2),
                })
            },
            {
                Difficulty.HARD,
                new(new(){
                    new (Difficulty.NORMAL,0.39),
                    new (Difficulty.HARD,0.6),
                    new (Difficulty.LEGENDARY,0.01),
                })
            },
            {
                Difficulty.LEGENDARY,
                new(new(){
                    new (Difficulty.HARD,0.8),
                    new (Difficulty.LEGENDARY,0.2),
                })
            },
        };

        public static readonly Dictionary<Difficulty, int> EnemyHPByDifficulty = new(){
            {Difficulty.EASY,4},
            {Difficulty.NORMAL,10},
            {Difficulty.HARD,20},
            {Difficulty.LEGENDARY,40},
        };
    }
}