using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.Entities
{
    public class Quest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Difficulty { get; set; }
        public string BatchId { get; set; }
    }
}