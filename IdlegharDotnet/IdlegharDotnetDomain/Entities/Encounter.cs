namespace IdlegharDotnetDomain.Entities
{
    public abstract class Encounter : Entity
    {
        public abstract EncounterState GetNewState();

        public abstract bool ProcessTick(Character character);
    }
}