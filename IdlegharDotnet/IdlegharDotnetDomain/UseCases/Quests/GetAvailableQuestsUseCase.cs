using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.UseCases.Quests
{
    public class GetAvailableQuestsUseCase
    {
        private IRandomnessProvider RandomnessProvider;
        private IQuestsProvider QuestsProvider;
        private ITimeProvider TimeProvider;

        public GetAvailableQuestsUseCase(IRandomnessProvider randomnessProvider, IQuestsProvider questsProvider, ITimeProvider timeProvider)
        {
            QuestsProvider = questsProvider;
            RandomnessProvider = randomnessProvider;
            TimeProvider = timeProvider;
        }

        public async Task<List<Quest>> Handle()
        {
            var currentBatch = await QuestsProvider.GetCurrentQuestBatch();
            if (IsValidQuestBatch(currentBatch))
            {
                return currentBatch!.Quests;
            }
            var quests = new List<Quest>();
            var batchId = Guid.NewGuid().ToString();
            quests.AddRange(CreateQuestsOfDifficulty(batchId, Constants.Difficulties.EASY, RandomnessProvider.GetRandomInt(1, 3)));
            quests.AddRange(CreateQuestsOfDifficulty(batchId, Constants.Difficulties.NORMAL, RandomnessProvider.GetRandomInt(2, 4)));
            quests.AddRange(CreateQuestsOfDifficulty(batchId, Constants.Difficulties.HARD, RandomnessProvider.GetRandomInt(1, 3)));
            quests.AddRange(CreateQuestsOfDifficulty(batchId, Constants.Difficulties.LEGENDARY, 1));

            await QuestsProvider.UpdateQuestBatch(new QuestBatch
            {
                Id = batchId,
                CreatedAt = TimeProvider.GetCurrentTime(),
                Quests = quests
            });

            return quests;
        }

        private bool IsValidQuestBatch(QuestBatch? currentBatch)
        {
            if (currentBatch == null) return false;

            if (TimeProvider.HaveTicksPassed(currentBatch.CreatedAt, Constants.TimeDefinitions.QuestsRegenerationTimeInTicks))
            {
                return false;
            }

            return true;
        }

        private List<Quest> CreateQuestsOfDifficulty(string batchId, string difficulty, int questCount)
        {
            var quests = new List<Quest>(questCount);
            for (int i = 0; i < questCount; i++)
            {
                quests.Add(new Quest()
                {
                    Id = Guid.NewGuid().ToString(),
                    Difficulty = difficulty,
                    BatchId = batchId
                });
            }
            return quests;
        }
    }
}