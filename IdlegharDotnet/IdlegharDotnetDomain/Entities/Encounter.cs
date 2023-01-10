namespace IdlegharDotnetDomain.Entities
{
    public abstract class Encounter : Entity
    {
        public abstract EncounterState GetNewState();

        public abstract bool ProcessTick(Character character);

        public T GetStateOrThrow<T>(Character character) where T : class
        {
            T? state = character.CurrentEncounterState as T;
            if (state == null)
                throw new InvalidOperationException(Constants.ErrorMessages.INVALID_ENCOUNTER_STATE);
            return state;
        }
    }
}