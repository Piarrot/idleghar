using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Quests;

namespace IdlegharDotnetDomain.UseCases.Quests
{
    public class SelectQuestUseCase
    {
        private IQuestsProvider QuestsProvider;
        private IPlayersProvider PlayersProvider;
        private ITimeProvider TimeProvider;

        public SelectQuestUseCase(IPlayersProvider playersProvider, IQuestsProvider questsProvider, ITimeProvider timeProvider)
        {
            PlayersProvider = playersProvider;
            QuestsProvider = questsProvider;
            TimeProvider = timeProvider;
        }

        public async Task Handle(AuthenticatedRequest<SelectQuestUseCaseRequest> authRequest)
        {
            var currentCharacter = authRequest.CurrentPlayer.GetCharacterOrThrow();
            currentCharacter.ThrowIfQuesting();

            var quest = await QuestsProvider.FindById(authRequest.Request.QuestId);

            if (quest == null || !QuestsProvider.IsBatchCurrent(quest!.BatchId!, TimeProvider))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.INVALID_QUEST);
            }

            currentCharacter.StartQuest(quest);
            await PlayersProvider.Save(authRequest.CurrentPlayer);
        }
    }
}