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
            questBatch.Quests.AddRange(QuestFactory.CreateQuests(questBatch.Id, Constants.Difficulties.EASY, RandomnessProvider.Resolve(Constants.Quests.EasyQuestCount)));
            questBatch.Quests.AddRange(QuestFactory.CreateQuests(questBatch.Id, Constants.Difficulties.NORMAL, RandomnessProvider.Resolve(Constants.Quests.NormalQuestCount)));
            questBatch.Quests.AddRange(QuestFactory.CreateQuests(questBatch.Id, Constants.Difficulties.HARD, RandomnessProvider.Resolve(Constants.Quests.HardQuestCount)));
            questBatch.Quests.AddRange(QuestFactory.CreateQuests(questBatch.Id, Constants.Difficulties.LEGENDARY, RandomnessProvider.Resolve(Constants.Quests.LegendaryQuestCount)));
            await QuestsProvider.SaveQuestBatch(questBatch);
            return questBatch;
        }
    }
}