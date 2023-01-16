using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Quests;

namespace IdlegharDotnetDomain.UseCases.Quests
{
    public class SelectQuestUseCase
    {
        private IQuestsProvider QuestsProvider;
        private ICharactersProvider CharactersProvider;
        private ITimeProvider TimeProvider;

        public SelectQuestUseCase(IQuestsProvider questsProvider, ITimeProvider timeProvider, ICharactersProvider charactersProvider)
        {
            QuestsProvider = questsProvider;
            TimeProvider = timeProvider;
            CharactersProvider = charactersProvider;
        }

        public async Task Handle(AuthenticatedRequest<SelectQuestUseCaseRequest> authRequest)
        {
            var currentCharacter = await CharactersProvider.GetCharacterFromPlayerOrThrow(authRequest.CurrentPlayer);
            currentCharacter.ThrowIfQuesting();

            var quest = await QuestsProvider.FindById(authRequest.Request.QuestId);

            if (quest == null || !QuestsProvider.IsBatchCurrent(quest!.BatchId!, TimeProvider))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.INVALID_QUEST);
            }

            currentCharacter.StartQuest(quest);
            await CharactersProvider.Save(currentCharacter);
        }
    }
}