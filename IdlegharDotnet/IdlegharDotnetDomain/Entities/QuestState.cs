using System.Collections.ObjectModel;
using IdlegharDotnetDomain.Entities.Encounters;
using IdlegharDotnetDomain.Entities.Encounters.Events;

namespace IdlegharDotnetDomain.Entities
{
    [Serializable()]
    public class QuestState : Entity
    {
        public Quest Quest { get; private set; }
        public Character Character { get; }

        List<EncounterState> previous = new();
        EncounterState current;
        List<EncounterState> remaining = new();
        List<EncounterEvent> questEvents { get; set; } = new();
        public ReadOnlyCollection<EncounterEvent> QuestEvents => questEvents.AsReadOnly();
        public bool Completed => this.current.Completed && this.remaining.Count == 0;
        public EncounterState CurrentEncounterState => this.current;

        public QuestState(Quest quest, Character character)
        {
            this.Quest = quest;
            Character = character;
            this.current = quest.Encounters.First().GetNewState();
            this.remaining = quest.Encounters.Skip(1).Select((e) => e.GetNewState()).ToList();
        }


        public EncounterType GetCurrentEncounterStateOrThrow<EncounterType>() where EncounterType : EncounterState
        {
            EncounterType? state = this.current as EncounterType;
            if (state == null)
                throw new InvalidOperationException(Constants.ErrorMessages.INVALID_ENCOUNTER_STATE);
            return state!;
        }

        public void AddEvent(EncounterEvent newEvent)
        {
            this.questEvents.Add(newEvent);
        }

        internal void ProcessTick()
        {
            var result = current.Encounter.ProcessEncounter(Character);
            if (result == EncounterResult.Succeeded)
            {
                if (!this.Completed)
                {
                    AdvanceToNextEncounter();
                }
            }
        }

        private void AdvanceToNextEncounter()
        {
            this.previous.Add(current);
            this.current = this.remaining.First();
            this.remaining.RemoveAt(0);
        }
    }
}