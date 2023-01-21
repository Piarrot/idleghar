using IdlegharDotnetDomain.Entities.Quests;

namespace IdlegharDotnetDomain.Entities
{
    [Serializable()]
    public class Character : Entity
    {
        public Player Player { get; private set; }
        public string Name { get; set; } = String.Empty;
        public QuestState? CurrentQuestState { get; private set; }
        public bool IsQuesting => CurrentQuestState != null;

        public int Level { get; private set; } = 1;
        public int XP { get; private set; } = 0;
        public int HP { get; private set; } = 1;
        public int MaxHP => Toughness * Constants.Characters.TOUGHNESS_TO_MAX_HP_MULTIPLIER;
        public int Damage { get; private set; } = 6;
        public int Toughness { get; private set; } = 1;

        public List<QuestState> QuestHistory { get; internal set; } = new();

        public Character(Player player)
        {
            this.HP = this.MaxHP;
            this.Player = player;
        }

        public Quest GetCurrentQuestOrThrow()
        {
            ThrowIfNotQuesting();
            return CurrentQuestState!.Quest;
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

        public void QuestDone()
        {
            this.QuestHistory.Add(CurrentQuestState!);
            this.CurrentQuestState = null;
        }
    }
}