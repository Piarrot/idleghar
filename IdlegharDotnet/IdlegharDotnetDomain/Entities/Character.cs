namespace IdlegharDotnetDomain.Entities
{
    public class Character
    {
        public string Id { get; internal set; }
        public string Name { get; set; }
        public Quest CurrentQuest { get; set; }
    }
}