using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.Tests.MockProviders
{
    public class MockQuestsProvider : IQuestsProvider
    {
        Dictionary<string, Quest> questsById = new Dictionary<string, Quest>();
        QuestBatch? CurrentBatch = null;

        public async Task<Quest?> FindById(string questId)
        {
            await Task.Yield();
            Quest? result = null;
            questsById.TryGetValue(questId, out result);
            return result;
        }

        public async Task<QuestBatch?> GetCurrentQuestBatch()
        {
            await Task.Yield();
            return CurrentBatch;
        }

        public bool IsBatchCurrent(string batchId, ITimeProvider timeProvider)
        {
            if (this.CurrentBatch == null || this.CurrentBatch.Id != batchId) return false;
            return this.CurrentBatch.IsValid(timeProvider);
        }

        public async Task SaveQuestBatch(QuestBatch batch)
        {
            await Task.Yield();
            CurrentBatch = batch;
            foreach (var quest in CurrentBatch.Quests)
            {
                questsById.Add(quest.Id, quest);
            }
        }
    }
}