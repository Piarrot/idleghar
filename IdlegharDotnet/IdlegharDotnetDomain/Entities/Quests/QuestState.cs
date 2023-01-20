using IdlegharDotnetDomain.Entities.Encounters;

namespace IdlegharDotnetDomain.Entities.Quests
{
    [Serializable()]
    public class QuestState : Entity
    {
        public Quest Quest { get; private set; }
        public Character Character { get; }

        List<EncounterState> previous = new();
        Stack<Encounter> remaining = new();

        public bool Completed => remaining.Count == 0;
        public bool Done { get; private set; }
        public QuestStatus Status { get; private set; } = QuestStatus.Pending;

        public float Progress => 1 - (float)this.remaining.Count / Constants.Quests.EncountersPerQuest;

        public QuestState(Quest quest, Character character)
        {
            Quest = quest;
            Character = character;
            remaining = new(quest.Encounters);
        }

        public void ProcessTick()
        {
            if (Completed) throw new InvalidOperationException(Constants.ErrorMessages.INVALID_QUEST);

            var current = remaining.Pop().ProcessEncounter(Character);

            this.previous.Add(current);

            if (current.Result == EncounterResult.Succeeded)
            {
                if (Completed)
                {
                    Status = QuestStatus.Succeeded;
                    Character.QuestDone();
                }
            }
            else
            {
                Status = QuestStatus.Failed;
                Character.QuestDone();
            }
        }
    }
}