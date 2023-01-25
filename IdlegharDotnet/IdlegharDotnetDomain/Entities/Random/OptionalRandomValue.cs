using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.Entities.Random
{
    public class OptionalRandomValue<T> : RandomValue<T?>
    {
        public override bool Matches(T? value)
        {
            throw new NotImplementedException();
        }

        public override T? ResolveOne(IRandomnessProvider randProvider)
        {
            throw new NotImplementedException();
        }
    }
}