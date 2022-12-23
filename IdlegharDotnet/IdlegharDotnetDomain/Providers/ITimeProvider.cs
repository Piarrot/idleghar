namespace IdlegharDotnetDomain.Providers
{
    public interface ITimeProvider
    {
        DateTime GetCurrentTime();

        bool HaveTicksPassed(DateTime startDate, int ticks);
    }
}