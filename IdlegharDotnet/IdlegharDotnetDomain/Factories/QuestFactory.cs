using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Entities.Encounters;

namespace IdlegharDotnetDomain.Factories
{
    public class QuestFactory
    {
        public static List<Quest> CreateQuests(string batchId, string difficulty, int questCount)
        {
            var quests = new List<Quest>(questCount);
            for (int i = 0; i < questCount; i++)
            {
                quests.Add(CreateQuest(batchId, difficulty));
            }
            return quests;
        }

        public static Quest CreateQuest(string batchId, string difficulty)
        {
            var quest = new Quest()
            {
                Difficulty = difficulty,
                BatchId = batchId
            };

            for (int i = 0; i < Constants.Quests.EncountersPerQuest; i++)
            {
                quest.Encounters.Add(new CombatEncounter());
            }

            return quest;
        }
    }
}