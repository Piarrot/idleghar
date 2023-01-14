using IdlegharDotnetDomain.Entities.Encounters;

namespace IdlegharDotnetDomain.Entities
{
    [Serializable()]
    public class Character : Entity
    {
        public string Name { get; set; } = String.Empty;
        public QuestState? CurrentQuestState { get; private set; }
        public bool IsQuesting => CurrentQuestState != null;
        public int HP { get; private set; } = 10;
        public int Damage { get; private set; } = 1;

        public Quest GetCurrentQuestOrThrow()
        {
            ThrowIfNotQuesting();
            return CurrentQuestState!.Quest;
        }

        public Encounter GetEncounterOrThrow()
        {
            ThrowIfNotQuesting();
            return CurrentQuestState!.CurrentEncounterState.Encounter;
        }

        public QuestState GetQuestStateOrThrow()
        {
            ThrowIfNotQuesting();
            return CurrentQuestState!;
        }

        public void ThrowIfNotQuesting()
        {
            if (!IsQuesting)
                throw new InvalidOperationException(Constants.ErrorMessages.CHARACTER_NOT_QUESTING);
        }
        public void ThrowIfQuesting()
        {
            if (IsQuesting)
                throw new InvalidOperationException(Constants.ErrorMessages.CHARACTER_ALREADY_QUESTING);
        }

        public void ReceiveDamage(int damage)
        {
            this.HP -= damage;
        }

        public void StartQuest(Quest quest)
        {
            this.CurrentQuestState = quest.GetNewState(this);
        }
    }
}