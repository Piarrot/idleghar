namespace IdlegharDotnetShared.Quests
{
    public class QuestStateViewModel
    {
        public QuestViewModel Quest { get; set; }
        public float Progress { get; set; }
        public List<EncounterStateViewModel> PreviousEncounters { get; set; } = new();

        public QuestStateViewModel(QuestViewModel quest)
        {
            Quest = quest;
        }
    }
}