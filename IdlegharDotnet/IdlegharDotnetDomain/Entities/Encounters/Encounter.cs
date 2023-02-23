using IdlegharDotnetDomain.Entities.Rewards;
using IdlegharDotnetShared.Constants;

namespace IdlegharDotnetDomain.Entities.Encounters
{
    [Serializable()]
    public abstract class Encounter : Entity
    {
        public Difficulty Difficulty { get; set; }
        public Reward Reward { get; internal set; } = new();

        public Encounter(Difficulty difficulty = Difficulty.EASY)
        {
            Difficulty = difficulty;
        }

        protected abstract EncounterState DoProcessEncounter(Character character);

        public EncounterState ProcessEncounter(Character character)
        {
            var state = this.DoProcessEncounter(character);

            if (state.Result == EncounterResult.Succeeded)
            {
                character.Owner.UnclaimedRewards.Add(this.Reward);
            }

            return state;
        }
    }
}