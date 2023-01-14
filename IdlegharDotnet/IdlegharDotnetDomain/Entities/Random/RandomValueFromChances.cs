using IdlegharDotnetDomain.Providers;
using IdlegharDotnetDomain.Utils;

namespace IdlegharDotnetDomain.Entities.Random
{
    public class RandomValueFromChances<T> : RandomValue<T>
    {
        List<ValueChance<T>> ValueChances = new List<ValueChance<T>>();

        public RandomValueFromChances(List<ValueChance<T>> valueChances)
        {
            this.ValueChances = valueChances;

            if (!FloatUtils.Equals(valueChances.Sum(c => c.Chance), 1))
            {
                throw new ArgumentException(Constants.ErrorMessages.CHANCES_DON_T_REACH_100);
            }
        }

        public override bool Matches(T value)
        {
            return this.ValueChances.Any((v) => v.Value!.Equals(value));
        }

        public bool Matches(T value, double chance)
        {
            var exists = this.ValueChances.Any((v) => v.Value!.Equals(value) && FloatUtils.Equals(v.Chance, chance, 0.05));
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