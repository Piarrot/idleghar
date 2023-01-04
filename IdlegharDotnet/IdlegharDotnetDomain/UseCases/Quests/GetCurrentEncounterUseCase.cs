using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetDomain.Utils;
using IdlegharDotnetShared;

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
            Assertions.UserHasCharacter(req.CurrentUser);
            Assertions.AssertCharacterIsQuesting(req.CurrentUser.Character!);

            return req.CurrentUser.Character!.CurrentEncounter!;
        }
    }
}