namespace IdlegharDotnetDomain.Entities
{
    public class Character : Entity
    {
        public string Name { get; set; }
        public Quest? CurrentQuest { get; set; }
        public Encounter? CurrentEncounter { get; set; }
        public bool IsQuesting
        {
            get
            {
                return CurrentQuest != null && CurrentEncounter != null;
            }
        }
    }
}