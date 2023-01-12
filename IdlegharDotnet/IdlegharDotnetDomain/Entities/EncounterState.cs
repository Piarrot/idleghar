using IdlegharDotnetDomain.Entities.Encounters;

namespace IdlegharDotnetDomain.Entities
{
    public abstract class EncounterState
    {
        public Encounter Encounter { get; protected set; }

        protected EncounterState(Encounter encounter)
        {
            this.Encounter = encounter;
        }
    }
}