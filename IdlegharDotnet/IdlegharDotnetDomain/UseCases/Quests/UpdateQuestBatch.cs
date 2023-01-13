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

            foreach (Constants.Difficulty questDifficulty in Enum.GetValues(typeof(Constants.Difficulty)))
            {
                questBatch.Quests.AddRange(QuestFactory.CreateQuests(questBatch.Id, questDifficulty, RandomnessProvider.Resolve(Constants.Quests.QuestCountByDifficulty[questDifficulty])));
            }
            await QuestsProvider.SaveQuestBatch(questBatch);
            return questBatch;
        }
    }
}