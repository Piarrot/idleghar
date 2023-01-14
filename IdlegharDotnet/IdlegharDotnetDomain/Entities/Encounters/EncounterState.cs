namespace IdlegharDotnetDomain.Entities.Encounters
{
    [Serializable()]
    public abstract class EncounterState
    {
        public Encounter Encounter { get; protected set; }
        public EncounterResult Result { get; set; }
        public bool Completed => Result != EncounterResult.Pending;

        protected EncounterState(Encounter encounter)
        {
            Encounter = encounter;
        }
    }
}