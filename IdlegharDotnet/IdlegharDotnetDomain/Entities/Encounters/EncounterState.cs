using System.Collections.ObjectModel;
using IdlegharDotnetShared.Constants;
using IdlegharDotnetShared.Events;

namespace IdlegharDotnetDomain.Entities.Encounters
{
    [Serializable()]
    public abstract class EncounterState
    {
        public Encounter Encounter { get; protected set; }
        public EncounterResult Result { get; set; }
        public bool Completed => Result != EncounterResult.Pending;
        List<EncounterEvent> events { get; set; } = new();
        public ReadOnlyCollection<EncounterEvent> EncounterEvents => events.AsReadOnly();

        public void AddEvent(EncounterEvent newEvent)
        {
            events.Add(newEvent);
        }

        protected EncounterState(Encounter encounter)
        {
            Encounter = encounter;
        }
    }
}