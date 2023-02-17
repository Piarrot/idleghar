using IdlegharDotnetDomain.Entities.Encounters;
using IdlegharDotnetDomain.Entities.Quests;
using IdlegharDotnetDomain.Entities.Rewards;
using IdlegharDotnetShared.Constants;

namespace IdlegharDotnetDomain.Entities
{
    [Serializable()]
    public class Quest : Entity
    {
        public string? Name { get; set; }
        public Difficulty Difficulty { get; set; }
        public string? BatchId { get; set; }
        public List<Encounter> Encounters { get; set; } = new List<Encounter>();
        public Reward Reward { get; set; } = new();

        public QuestState GetNewState(Character character)
        {
            return new QuestState(this, character);
        }
    }
}