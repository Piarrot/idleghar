using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Entities.Encounters;
using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.UseCases.Quests
{
    public class GetCurrentEncounterUseCase
    {
        private IPlayersProvider PlayersProvider;
        private ICharactersProvider CharactersProvider;

        public GetCurrentEncounterUseCase(IPlayersProvider playersProvider, ICharactersProvider charactersProvider)
        {
            PlayersProvider = playersProvider;
            CharactersProvider = charactersProvider;
        }

        public async Task<Encounter> Handle(AuthenticatedRequest req)
        {
            Character playersCharacter = await CharactersProvider.GetCharacterFromPlayerOrThrow(req.CurrentPlayer);

            Encounter currentEncounter = playersCharacter.GetEncounterOrThrow();

            return currentEncounter;
        }
    }
}