using IdlegharDotnetDomain.Entities.Random;

namespace IdlegharDotnetDomain.Providers
{
    public class RandomnessProvider : IRandomnessProvider
    {
        private Random RNG;
        public string Seed { get; private set; }

        public RandomnessProvider(string seed)
        {
            this.Seed = seed;
            this.RNG = new Random(seed.GetHashCode());
        }

        public int GetRandomInt(int min, int max)
        {
            return RNG.Next(min, max + 1);
        }

        public T Resolve<T>(RandomValue<T> range)
        {
            return range.Resolve(this);
        }

        public double GetRandomDouble(double min, double max)
        {
            return min + (RNG.NextDouble() * max);
        }
    }
}