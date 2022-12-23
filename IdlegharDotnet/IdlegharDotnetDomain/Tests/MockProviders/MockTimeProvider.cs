using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.Tests.MockProviders
{
    public class MockTimeProvider : ITimeProvider
    {
        private int offsetMinutes = 0;

        public DateTime GetCurrentTime()
        {
            return DateTime.UtcNow.AddMinutes(offsetMinutes);
        }

        public bool HaveTicksPassed(DateTime startDate, int ticks)
        {
            var now = GetCurrentTime();
            var maxTimeSpan = TimeSpan.FromMinutes(TicksToMinutes(ticks));
            var currentTimeSpan = now - startDate;
            return currentTimeSpan >= maxTimeSpan;
        }

        public void MoveTimeInTicks(int ticks)
        {
            offsetMinutes += TicksToMinutes(ticks);
        }

        private int TicksToMinutes(int ticks)
        {
            return Constants.TimeDefinitions.TickInMinutes * ticks;
        }
    }
}