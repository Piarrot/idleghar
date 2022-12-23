using IdlegharDotnetDomain.Entities;

namespace IdlegharDotnetDomain.Providers
{
    public interface IQuestsProvider
    {
        Task<QuestBatch?> GetCurrentQuestBatch();
        Task UpdateQuestBatch(QuestBatch batch);
    }
}