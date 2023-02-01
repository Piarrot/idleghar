using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.Entities.Random
{
    public abstract class OptionalRandomValue<T> : RandomValue<Optional<T>>
    {
        public override abstract bool Matches(Optional<T> value);

        public override abstract Optional<T> ResolveOne(IRandomnessProvider randProvider);
    }
}