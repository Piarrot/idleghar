using IdlegharDotnetDomain.Entities;

namespace IdlegharDotnetDomain.Providers
{
    public interface IQuestsProvider
    {
        Task<Quest?> FindById(string questId);
        Task<QuestBatch?> GetCurrentQuestBatch();
        Task UpdateQuestBatch(QuestBatch batch);
        bool IsBatchCurrent(string batchId, ITimeProvider timeProvider);
    }
}