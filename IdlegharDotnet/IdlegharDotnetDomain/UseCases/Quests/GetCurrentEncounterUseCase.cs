using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Entities.Encounters;
using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.UseCases.Quests
{
    public class GetCurrentEncounterUseCase
    {
        private IPlayersProvider PlayersProvider;

        public GetCurrentEncounterUseCase(IPlayersProvider playersProvider)
        {
            PlayersProvider = playersProvider;
        }

        public Encounter Handle(AuthenticatedRequest req)
        {
            Character playersCharacter = req.CurrentPlayer.GetCharacterOrThrow();

            Encounter currentEncounter = playersCharacter.GetEncounterOrThrow();

            return currentEncounter;
        }
    }
}