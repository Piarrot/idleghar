using NUnit.Framework;

namespace IdlegharDotnetDomain.Utils.Tests
{
    public class FloatUtilsTests
    {
        [Test]
        public void ItShouldWorkProperly()
        {
            double x = 0.9999999999999999;
            double y = 1;

            Assert.That(MathUtils.Equals(x, y), Is.True);

            x = 1;
            y = 1;
            Assert.That(MathUtils.Equals(x, y), Is.True);
        }
    }
}