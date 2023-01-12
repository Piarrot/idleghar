using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.Entities.Random
{
    public class RandomIntRange : RandomValue<int>
    {
        private int Min;
        private int Max;

        public RandomIntRange(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public override int Resolve(IRandomnessProvider randProvider)
        {
            return randProvider.GetRandomInt(Min, Max);
        }

        public override bool Matches(int value)
        {
            return Min <= value && value <= Max;
        }
    }
}