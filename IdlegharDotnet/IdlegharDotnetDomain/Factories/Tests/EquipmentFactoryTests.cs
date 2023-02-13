using IdlegharDotnetDomain.Providers;
using IdlegharDotnetDomain.Tests;
using IdlegharDotnetShared.Constants;
using Moq;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Factories.Tests
{
    public class EquipmentFactoryTests : BaseTests
    {
        [Test]
        [TestCase(ItemQuality.Common, 1, 1)]
        [TestCase(ItemQuality.Exceptional, 3, 3)]
        [TestCase(ItemQuality.Exceptional, 3, 1)]
        public void GetEquipmentStatsShouldReturnValidValues(ItemQuality quality, int value, int step)
        {
            RandomnessProviderMock.SetupSequence(MockRandomIntLambda).Returns(value).Returns(value).Returns(0);
            var factory = new EquipmentFactory(RandomnessProviderMock.Object);
            var stats = factory.GetEquipmentStats(quality);
            Assert.That(stats.GetStat(Constants.Characters.Stat.DAMAGE), Is.EqualTo(value));
        }
    }
}