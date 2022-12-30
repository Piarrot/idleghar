using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Quests;

namespace IdlegharDotnetDomain.UseCases.Quests
{
    public class SelectQuestUseCase
    {
        private IQuestsProvider QuestsProvider;
        private IUsersProvider UsersProvider;
        private ITimeProvider TimeProvider;

        public SelectQuestUseCase(IUsersProvider usersProvider, IQuestsProvider questsProvider, ITimeProvider timeProvider)
        {
            UsersProvider = usersProvider;
            QuestsProvider = questsProvider;
            TimeProvider = timeProvider;
        }

        public async Task Handle(AuthenticatedRequest<SelectQuestUseCaseRequest> authRequest)
        {
            if (authRequest.CurrentUser.Character == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.CHARACTER_NOT_CREATED);
            }

            var quest = await QuestsProvider.FindById(authRequest.Request.QuestId);

            if (quest == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.INVALID_QUEST);
            }

            if (!QuestsProvider.IsBatchCurrent(quest.BatchId, TimeProvider))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.INVALID_QUEST);
            }

            authRequest.CurrentUser.Character!.CurrentQuest = quest;
            await UsersProvider.Save(authRequest.CurrentUser);
        }
    }
}