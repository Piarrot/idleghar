using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.Entities
{
    public class QuestBatch : Entity
    {
        public DateTime CreatedAt { get; set; }
        public List<Quest> Quests { get; set; } = new List<Quest>();

        public bool IsValid(ITimeProvider timeProvider)
        {
            return !timeProvider.HaveTicksPassed(CreatedAt, Constants.TimeDefinitions.QuestsRegenerationTimeInTicks);
        }
    }
}