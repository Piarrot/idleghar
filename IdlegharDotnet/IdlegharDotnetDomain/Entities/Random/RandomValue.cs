using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.Entities.Random
{
    public abstract class RandomValue<T>
    {
        public abstract T Resolve(IRandomnessProvider randProvider);

        public abstract bool Matches(T value);
    }
}