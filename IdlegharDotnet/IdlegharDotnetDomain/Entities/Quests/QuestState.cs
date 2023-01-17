using System.Collections.ObjectModel;
using IdlegharDotnetDomain.Entities.Encounters;
using IdlegharDotnetDomain.Entities.Encounters.Events;

namespace IdlegharDotnetDomain.Entities.Quests
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
        public bool Completed => current.Completed && remaining.Count == 0;
        public bool Done { get; private set; }
        public EncounterState CurrentEncounterState => current;
        public QuestStatus Status { get; private set; } = QuestStatus.Pending;

        public QuestState(Quest quest, Character character)
        {
            Quest = quest;
            Character = character;
            current = quest.Encounters.First().GetNewState();
            remaining = quest.Encounters.Skip(1).Select((e) => e.GetNewState()).ToList();
        }


        public EncounterType GetCurrentEncounterStateOrThrow<EncounterType>() where EncounterType : EncounterState
        {
            EncounterType? state = current as EncounterType;
            if (state == null)
                throw new InvalidOperationException(Constants.ErrorMessages.INVALID_ENCOUNTER_STATE);
            return state!;
        }

        public void AddEvent(EncounterEvent newEvent)
        {
            questEvents.Add(newEvent);
        }

        public void ProcessTick()
        {
            var result = current.Encounter.ProcessEncounter(Character);
            if (result == EncounterResult.Succeeded)
            {
                if (!Completed)
                {
                    AdvanceToNextEncounter();
                    return;
                }
                else
                {
                    Status = QuestStatus.Succeeded;
                    Character.QuestDone();
                }
            }
            else
            {
                Status = QuestStatus.Failed;
                Character.QuestDone();
            }

        }

        private void AdvanceToNextEncounter()
        {
            previous.Add(current);
            current = remaining.First();
            remaining.RemoveAt(0);
        }
    }
}