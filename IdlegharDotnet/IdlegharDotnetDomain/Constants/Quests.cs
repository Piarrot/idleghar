using IdlegharDotnetDomain.Entities.Random;
using IdlegharDotnetShared.Constants;

namespace IdlegharDotnetDomain.Constants
{
    public class Quests
    {
        public static readonly int EncountersPerQuest = 7;

        // Specs for quests random count: 
        // https://docs.google.com/document/d/1loOzBcBmZVcGhZWQc69hp5oNjqyMfc-GvOeIxjzq_9M/edit#heading=h.obgpyyfs06mo
        public static readonly Dictionary<Difficulty, RandomValue<int>> QuestCountByDifficulty = new(){
            { Difficulty.EASY, new RandomIntRange(1,3) },
            { Difficulty.NORMAL, new RandomIntRange(2,4) },
            { Difficulty.HARD, new RandomIntRange(1,3) },
            { Difficulty.LEGENDARY, new RandomIntRange(1,1) },
        };
    }
}