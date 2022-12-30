using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.Entities
{
    public class QuestBatch
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Quest> Quests { get; set; }

        public bool IsValid(ITimeProvider timeProvider)
        {
            return !timeProvider.HaveTicksPassed(CreatedAt, Constants.TimeDefinitions.QuestsRegenerationTimeInTicks);
        }
    }
}