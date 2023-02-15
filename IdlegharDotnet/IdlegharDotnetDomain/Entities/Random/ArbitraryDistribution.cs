using IdlegharDotnetDomain.Providers;
using IdlegharDotnetDomain.Utils;

namespace IdlegharDotnetDomain.Entities.Random
{
    public class ArbitraryDistribution<T> : RandomValue<T>
    {
        List<ArbitraryChance<T>> ValueChances = new List<ArbitraryChance<T>>();

        public double this[T ChanceValue]
        {
            get
            {
                return ValueChances.Find((vc) => vc.Value!.Equals(ChanceValue))?.Chance ?? 0;
            }
            set
            {
                var existing = ValueChances.Find((vc) => vc.Value!.Equals(ChanceValue));
                if (existing != null)
                {
                    existing.Chance = value;
                }
                else
                {
                    ValueChances.Add(new(ChanceValue, value));
                }
            }
        }

        public override bool Matches(T value)
        {
            return this.ValueChances.Any((v) => v.Value!.Equals(value));
        }

        public bool Matches(T value, double chance)
        {
            var exists = this.ValueChances.Any((v) => v.Value!.Equals(value) && MathUtils.Equals(v.Chance, chance, 0.05));
            if (exists) return true;
            return chance == 0;
        }

        public override T ResolveOne(IRandomnessProvider randProvider)
        {
            var randValue = randProvider.GetRandomDouble(0, 1);
            var currentMin = 0.0;
            foreach (var valueChance in ValueChances)
            {
                if (currentMin <= randValue && randValue < (currentMin + valueChance.Chance))
                {
                    return valueChance.Value;
                }
                currentMin += valueChance.Chance;
            }
            return ValueChances.Last().Value;
        }

        internal double GetChance(T value)
        {
            return this.ValueChances.Find((v) => v.Value!.Equals(value))!.Chance;
        }
    }
}