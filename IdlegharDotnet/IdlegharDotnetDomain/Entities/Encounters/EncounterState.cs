using System.Collections.ObjectModel;
using IdlegharDotnetDomain.Entities.Encounters.Events;

namespace IdlegharDotnetDomain.Entities.Encounters
{
    [Serializable()]
    public abstract class EncounterState
    {
        public Encounter Encounter { get; protected set; }
        public EncounterResult Result { get; set; }
        public bool Completed => Result != EncounterResult.Pending;
        List<EncounterEvent> encounterEvents { get; set; } = new();
        public ReadOnlyCollection<EncounterEvent> EncounterEvents => encounterEvents.AsReadOnly();

        public void AddEvent(EncounterEvent newEvent)
        {
            encounterEvents.Add(newEvent);
        }

        protected EncounterState(Encounter encounter)
        {
            Encounter = encounter;
        }
    }
}