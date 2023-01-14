using IdlegharDotnetDomain.Constants;

namespace IdlegharDotnetDomain.Entities.Encounters
{
    [Serializable()]
    public abstract class Encounter : Entity
    {
        public Difficulty Difficulty { get; set; }
        public abstract EncounterState GetNewState();

        public Encounter(Difficulty difficulty = Difficulty.EASY)
        {
            Difficulty = difficulty;
        }

        public abstract EncounterResult ProcessEncounter(Character character);
    }
}