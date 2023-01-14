using IdlegharDotnetDomain.Entities.Encounters;
using IdlegharDotnetDomain.Entities.Encounters.Events;

namespace IdlegharDotnetDomain.Entities
{
    [Serializable()]
    public class Character : Entity
    {
        public string Name { get; set; } = String.Empty;
        public Quest? CurrentQuest { get; set; }
        public EncounterState? CurrentEncounterState { get; set; }
        public bool IsQuesting => CurrentQuest != null && CurrentEncounterState != null;

        public int HP { get; private set; } = 10;

        public int Damage { get; private set; } = 1;
        public List<EncounterEvent> CurrentQuestEvents { get; private set; } = new List<EncounterEvent>();

        public Encounter GetEncounterOrThrow()
        {
            if (CurrentQuest == null || CurrentEncounterState == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.CHARACTER_NOT_QUESTING);
            }
            return CurrentEncounterState.Encounter;
        }

        public void ThrowIfNotQuesting()
        {
            if (CurrentQuest != null || CurrentEncounterState != null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.CHARACTER_ALREADY_QUESTING);
            }
        }

        public void ReceiveDamage(int damage)
        {
            this.HP -= damage;
        }
    }
}