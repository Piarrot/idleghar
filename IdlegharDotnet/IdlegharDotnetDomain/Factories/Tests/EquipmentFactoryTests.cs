using IdlegharDotnetDomain.Tests;
using IdlegharDotnetShared.Constants;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Factories.Tests
{
    public class EquipmentFactoryTests : BaseTests
    {
        [Test]
        [TestCase(ItemQuality.Common, 1)]
        [TestCase(ItemQuality.Exceptional, 3)]
        public void GetEquipmentStatsShouldReturnValidValues(ItemQuality quality, int value)
        {
            RandomnessProviderMock.SetupSequence(MockRandomIntLambda)
                .Returns(value)
                .Returns(value);
            var factory = new EquipmentFactory(RandomnessProviderMock.Object);
            var stats = factory.GetEquipmentStats(quality);
            Assert.That(stats.GetStat(Constants.Characters.Stat.DAMAGE), Is.EqualTo(value));
        }
    }
}