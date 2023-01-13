namespace IdlegharDotnetDomain.Entities.Random
{
    public class ValueChance<T>
    {
        public T Value { get; }
        public double Chance { get; set; }

        public ValueChance(T value, double chance)
        {
            this.Value = value;
            this.Chance = chance;
        }
    }
}