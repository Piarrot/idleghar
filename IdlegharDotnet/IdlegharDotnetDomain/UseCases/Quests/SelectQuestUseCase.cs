using IdlegharDotnetDomain.Providers;
using IdlegharDotnetShared.Quests;

namespace IdlegharDotnetDomain.UseCases.Quests
{
    public class SelectQuestUseCase
    {
        public SelectQuestUseCase(IQuestsProvider questsProvider, ITimeProvider timeProvider, IStorageProvider playersProvider)
        {
            QuestsProvider = questsProvider;
            TimeProvider = timeProvider;
            StorageProvider = playersProvider;
        }

        public IQuestsProvider QuestsProvider { get; }
        public ITimeProvider TimeProvider { get; }
        public IStorageProvider StorageProvider { get; }

        public async Task Handle(AuthenticatedRequest<SelectQuestUseCaseRequest> authRequest)
        {
            var character = await StorageProvider.GetCharacterByPlayerIdOrThrow(authRequest.CurrentPlayerCreds.Id);
            character.ThrowIfQuesting();

            var quest = await QuestsProvider.FindById(authRequest.Request.QuestId);

            if (quest == null || !QuestsProvider.IsBatchCurrent(quest!.BatchId!, TimeProvider))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.INVALID_QUEST);
            }

            character.StartQuest(quest);
            await StorageProvider.SaveCharacter(character);
        }
    }
}