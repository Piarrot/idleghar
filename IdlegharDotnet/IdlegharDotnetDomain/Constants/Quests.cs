using IdlegharDotnetDomain.Entities.Random;

namespace IdlegharDotnetDomain.Constants
{
    public class Quests
    {
        public static readonly int EncountersPerQuest = 7;

        // Specs for quests random count: 
        // https://docs.google.com/document/d/1loOzBcBmZVcGhZWQc69hp5oNjqyMfc-GvOeIxjzq_9M/edit#heading=h.obgpyyfs06mo
        public static readonly RandomValue<int> EasyQuestCount = new RandomIntRange(1, 3);
        public static readonly RandomValue<int> NormalQuestCount = new RandomIntRange(2, 4);
        public static readonly RandomValue<int> HardQuestCount = new RandomIntRange(1, 3);
        public static readonly RandomValue<int> LegendaryQuestCount = new RandomIntRange(1, 1);
    }
}