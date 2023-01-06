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
            var currentCharacter = authRequest.CurrentUser.GetCharacterOrThrow();
            currentCharacter.ThrowIfNotQuesting();

            var quest = await QuestsProvider.FindById(authRequest.Request.QuestId);

            if (quest == null || !QuestsProvider.IsBatchCurrent(quest!.BatchId!, TimeProvider))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.INVALID_QUEST);
            }

            currentCharacter!.CurrentQuest = quest;
            currentCharacter.CurrentEncounter = quest.Encounters[0];
            await UsersProvider.Save(authRequest.CurrentUser);
        }
    }
}