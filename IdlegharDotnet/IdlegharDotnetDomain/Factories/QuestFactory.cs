using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Constants;

namespace IdlegharDotnetDomain.Factories
{
    public class QuestFactory
    {
        private IRandomnessProvider RandomnessProvider;
        private ITimeProvider TimeProvider;

        public QuestFactory(IRandomnessProvider randomnessProvider, ITimeProvider timeProvider)
        {
            RandomnessProvider = randomnessProvider;
            TimeProvider = timeProvider;
        }

        public List<Quest> CreateQuests(string batchId, Difficulty difficulty, int questCount)
        {
            var quests = new List<Quest>(questCount);
            for (int i = 0; i < questCount; i++)
            {
                quests.Add(CreateQuest(batchId, difficulty));
            }
            return quests;
        }

        public Quest CreateQuest(string batchId, Difficulty difficulty)
        {
            var quest = new Quest()
            {
                Difficulty = difficulty,
                BatchId = batchId
            };

            var ef = new CombatEncounterFactory(RandomnessProvider);

            for (int i = 0; i < Constants.Quests.EncountersPerQuest; i++)
            {
                quest.Encounters.Add(ef.CreateCombatFromQuestDifficulty(difficulty));
            }

            var rewardFactory = new RewardFactory(RandomnessProvider);
            quest.Rewards = rewardFactory.CreateQuestRewards(difficulty);

            return quest;
        }

        public QuestBatch CreateQuestBatch()
        {
            var questBatch = new QuestBatch(TimeProvider);

            foreach (Difficulty questDifficulty in Enum.GetValues(typeof(Difficulty)))
            {
                var resolvedQuestCount = RandomnessProvider.ResolveOne(Constants.Quests.QuestCountByDifficulty[questDifficulty]);
                var createdQuests = CreateQuests(questBatch.Id, questDifficulty, resolvedQuestCount);
                questBatch.Quests.AddRange(createdQuests);
            }
            return questBatch;
        }
    }
}