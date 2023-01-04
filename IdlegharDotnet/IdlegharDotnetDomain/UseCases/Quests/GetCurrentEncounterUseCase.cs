using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;
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

        public async Task<Encounter> Handle(AuthenticatedRequest req)
        {
            if (req.CurrentUser.Character == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.CHARACTER_NOT_CREATED);
            }

            if (req.CurrentUser.Character.CurrentQuest == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.CHARACTER_NOT_QUESTING);
            }

            if (req.CurrentUser.Character.CurrentEncounter != null)
            {
                return req.CurrentUser.Character!.CurrentEncounter;
            }

            var encounter = new Encounter();
            req.CurrentUser.Character!.CurrentEncounter = encounter;

            await UsersProvider.Save(req.CurrentUser);

            return encounter;
        }
    }
}