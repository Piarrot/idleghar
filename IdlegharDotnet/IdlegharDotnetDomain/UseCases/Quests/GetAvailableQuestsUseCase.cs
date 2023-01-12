using IdlegharDotnetDomain.Entities;
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
                currentBatch = await (new UpdateQuestBatch(RandomnessProvider, QuestsProvider, TimeProvider).Handle());
            }
            return QuestTransformer.Transform(currentBatch!.Quests);
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
    }
}