namespace IdlegharDotnetDomain.Entities.Items
{
    [Serializable()]
    public abstract class Item : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        protected Item(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}