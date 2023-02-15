using IdlegharDotnetDomain.Providers;
using IdlegharDotnetDomain.Utils;

namespace IdlegharDotnetDomain.Entities.Random
{
    public class ArbitraryPartialDistribution<T> : OptionalRandomValue<T>
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

        public override bool Matches(Optional<T> value)
        {
            return this.ValueChances.Any((v) => v.Value!.Equals(value));
        }

        public bool Matches(Optional<T> option, double chance)
        {
            if (!option.HasValue) return true;
            var exists = this.ValueChances.Any((v) => v.Value!.Equals(option.Value) && MathUtils.Equals(v.Chance, chance, 0.05));
            if (exists) return true;
            return chance == 0;
        }

        public override Optional<T> ResolveOne(IRandomnessProvider randProvider)
        {
            var randValue = randProvider.GetRandomDouble(0, 1);
            var currentMin = 0.0;
            foreach (var valueChance in ValueChances)
            {
                if (currentMin <= randValue && randValue < (currentMin + valueChance.Chance))
                {
                    return Optional<T>.Some(valueChance.Value);
                }
                currentMin += valueChance.Chance;
            }
            return Optional<T>.None();
        }
    }
}