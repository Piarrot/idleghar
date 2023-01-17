using IdlegharDotnetDomain.Entities.Encounters;
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

        public QuestState GetNewState(Character character)
        {
            return new QuestState(this, character);
        }
    }
}