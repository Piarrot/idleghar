namespace IdlegharDotnetShared.Quests
{
    public class QuestStateViewModel
    {
        public QuestViewModel Quest { get; set; }

        public QuestStateViewModel(QuestViewModel quest)
        {
            Quest = quest;
        }
    }
}