using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Factories;
using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.UseCases.Quests
{
    public class UpdateQuestBatch
    {
        private IRandomnessProvider RandomnessProvider;
        private IQuestsProvider QuestsProvider;
        private ITimeProvider TimeProvider;

        public UpdateQuestBatch(IRandomnessProvider randomnessProvider, IQuestsProvider questsProvider, ITimeProvider timeProvider)
        {
            RandomnessProvider = randomnessProvider;
            QuestsProvider = questsProvider;
            TimeProvider = timeProvider;
        }

        public async Task<QuestBatch> Handle()
        {
            var questBatch = new QuestBatch(TimeProvider);
            questBatch.Quests.AddRange(QuestFactory.CreateQuestsOfDifficulty(questBatch.Id, Constants.Difficulties.EASY, RandomnessProvider.GetRandomInt(1, 3)));
            questBatch.Quests.AddRange(QuestFactory.CreateQuestsOfDifficulty(questBatch.Id, Constants.Difficulties.NORMAL, RandomnessProvider.GetRandomInt(2, 4)));
            questBatch.Quests.AddRange(QuestFactory.CreateQuestsOfDifficulty(questBatch.Id, Constants.Difficulties.HARD, RandomnessProvider.GetRandomInt(1, 3)));
            questBatch.Quests.AddRange(QuestFactory.CreateQuestsOfDifficulty(questBatch.Id, Constants.Difficulties.LEGENDARY, 1));
            await QuestsProvider.SaveQuestBatch(questBatch);
            return questBatch;
        }
    }
}