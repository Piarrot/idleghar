using IdlegharDotnetDomain.Entities.Random;

namespace IdlegharDotnetDomain.Providers
{
    public interface IRandomnessProvider
    {
        int GetRandomInt(int min, int max);

        T Resolve<T>(RandomValue<T> range);
    }
}