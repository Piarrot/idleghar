namespace IdlegharDotnetDomain.Entities.Random
{
    public class ArbitraryChance<T>
    {
        public T Value { get; }
        public double Chance { get; set; }

        public ArbitraryChance(T value, double chance)
        {
            this.Value = value;
            this.Chance = chance;
        }
    }
}