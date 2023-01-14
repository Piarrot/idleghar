using IdlegharDotnetDomain.Entities.Random;

namespace IdlegharDotnetDomain.Providers
{
    public interface IRandomnessProvider
    {
        double GetRandomDouble(double min, double max);
        int GetRandomInt(int min, int max);

        T ResolveOne<T>(RandomValue<T> range);
    }
}