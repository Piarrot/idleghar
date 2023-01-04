namespace IdlegharDotnetDomain.Entities
{
    public class Quest : Entity
    {
        public string? Name { get; set; }
        public string? Difficulty { get; set; }
        public string? BatchId { get; set; }
        public List<Encounter> Encounters { get; set; } = new List<Encounter>();
    }
}