using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Entities.Encounters;
using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.UseCases.Quests
{
    public class GetCurrentEncounterUseCase
    {
        private IUsersProvider UsersProvider;

        public GetCurrentEncounterUseCase(IUsersProvider usersProvider)
        {
            UsersProvider = usersProvider;
        }

        public Encounter Handle(AuthenticatedRequest req)
        {
            Character userCharacter = req.CurrentUser.GetCharacterOrThrow();

            Encounter currentEncounter = userCharacter.GetEncounterOrThrow();

            return currentEncounter;
        }
    }
}