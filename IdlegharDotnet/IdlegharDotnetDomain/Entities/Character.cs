using IdlegharDotnetDomain.Entities.Items;
using IdlegharDotnetDomain.Entities.Quests;
using IdlegharDotnetShared.SharedConstants;

namespace IdlegharDotnetDomain.Entities
{
    [Serializable()]
    public class Character : Entity
    {
        public Player Owner { get; private set; }
        public string Name { get; set; } = String.Empty;
        public QuestState? CurrentQuestState { get; private set; }
        public bool IsQuesting => CurrentQuestState != null;

        public int Level { get; private set; } = 1;
        public int XP { get; private set; } = 0;
        public int HP { get; private set; } = 1;
        public int MaxHP => Toughness * Constants.Characters.TOUGHNESS_TO_MAX_HP_MULTIPLIER;

        public int BaseDamage { get; private set; } = 6;

        public int PointsToLevelUp { get; private set; } = 0;

        public int Damage
        {
            get
            {
                return BaseDamage + Inventory.EquippedDamage;
            }
        }
        public int Toughness { get; private set; } = 1;

        public Inventory Inventory { get; private set; }

        public List<QuestState> QuestHistory { get; internal set; } = new();

        public Character(Player player)
        {
            this.HP = this.MaxHP;
            this.Inventory = new Inventory(this);
            this.Owner = player;
            player.Character = this;
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

        public Equipment? EquipItem(Equipment equipment)
        {
            return this.Inventory.EquipItem(equipment);
        }

        public int XPToNextLevel
        {
            get
            {
                return this.Level * 1000;
            }
        }

        public bool IsLevelingUp
        {
            get
            {
                return PointsToLevelUp > 0;
            }
        }

        public void AddXP(int xp)
        {
            this.XP += xp;
            if (XP >= this.XPToNextLevel)
            {
                this.XP -= this.XPToNextLevel;
                this.Level++;
                this.PointsToLevelUp += Constants.Characters.PointsPerLevelUp;
            }
        }

        public void AddAttrPoints(CharacterStat key, int value)
        {
            switch (key)
            {
                case CharacterStat.DAMAGE:
                    {
                        BaseDamage += value;
                        break;
                    }
                case CharacterStat.TOUGHNESS:
                    {
                        Toughness += value;
                        break;
                    }
                default:
                    {
                        throw new InvalidOperationException(Constants.ErrorMessages.INVALID_STAT);
                    }
            }

            this.PointsToLevelUp -= value;
        }
    }
}