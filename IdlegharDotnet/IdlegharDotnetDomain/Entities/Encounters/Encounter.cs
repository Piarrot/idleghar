using IdlegharDotnetShared.Constants;

namespace IdlegharDotnetDomain.Entities.Encounters
{
    [Serializable()]
    public abstract class Encounter : Entity
    {
        public Difficulty Difficulty { get; set; }

        public Encounter(Difficulty difficulty = Difficulty.EASY)
        {
            Difficulty = difficulty;
        }

        public abstract EncounterState ProcessEncounter(Character character);
    }
}