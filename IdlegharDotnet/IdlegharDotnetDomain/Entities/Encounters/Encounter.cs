namespace IdlegharDotnetDomain.Entities.Encounters
{
    [Serializable()]
    public abstract class Encounter : Entity
    {
        public abstract EncounterState GetNewState();

        public abstract EncounterResult ProcessEncounter(Character character);
    }
}