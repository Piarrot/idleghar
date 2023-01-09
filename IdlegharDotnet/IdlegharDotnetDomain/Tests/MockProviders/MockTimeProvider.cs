using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.Tests.MockProviders
{
    public class MockTimeProvider : ITimeProvider
    {
        private int offsetInSeconds = 0;

        public DateTime GetCurrentTime()
        {
            return DateTime.UtcNow.AddSeconds(offsetInSeconds);
        }

        public bool HaveTicksPassed(DateTime startDate, int ticks)
        {
            var now = GetCurrentTime();
            var maxTimeSpan = TimeSpan.FromSeconds(TicksToSeconds(ticks));
            var currentTimeSpan = now - startDate;
            return currentTimeSpan >= maxTimeSpan;
        }

        public void MoveTimeInTicks(int ticks)
        {
            offsetInSeconds += TicksToSeconds(ticks);
        }

        private int TicksToSeconds(int ticks)
        {
            return Constants.TimeDefinitions.TickInSeconds * ticks;
        }
    }
}