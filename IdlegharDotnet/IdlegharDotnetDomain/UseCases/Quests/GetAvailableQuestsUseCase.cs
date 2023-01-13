using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Factories;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetDomain.Transformers;
using IdlegharDotnetShared.Quests;

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

        public async Task<List<QuestViewModel>> Handle()
        {
            var currentBatch = await QuestsProvider.GetCurrentQuestBatch();
            if (!IsValidQuestBatch(currentBatch))
            {
                var factory = new QuestFactory(RandomnessProvider, TimeProvider);
                currentBatch = factory.CreateQuestBatch();
                await QuestsProvider.SaveQuestBatch(currentBatch);
            }
            return QuestTransformer.Transform(currentBatch!.Quests);
        }

        private bool IsValidQuestBatch(QuestBatch? currentBatch)
        {
            if (currentBatch == null) return false;

            if (!currentBatch.IsValid(TimeProvider))
            {
                return false;
            }

            return true;
        }
    }
}