namespace IdlegharDotnetDomain.Entities.Items
{
    [Serializable()]
    public class Weapon : Item
    {
        public Weapon(string name, string description) : base(name, description)
        {
        }
    }
}