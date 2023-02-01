using IdlegharDotnetDomain.Providers;
using Moq;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Entities.Random.Tests
{
    public class RandomValueFromChancesTests
    {
        [Test]
        public void ItShouldProperlyResolveChances()
        {
            var rngProviderMock = new Mock<IRandomnessProvider>();

            ArbitraryDistribution<string> valueFromChances = new()
            {
                ["hello"] = 0.6,
                ["banana"] = 0.3,
                ["world"] = 0.1
            };

            rngProviderMock.SetupSequence(x => x.GetRandomDouble(0, 1)).Returns(0.5).Returns(0.7).Returns(0.9);

            var result = valueFromChances.ResolveOne(rngProviderMock.Object);
            Assert.That(result, Is.EqualTo("hello"));

            result = valueFromChances.ResolveOne(rngProviderMock.Object);
            Assert.That(result, Is.EqualTo("banana"));

            result = valueFromChances.ResolveOne(rngProviderMock.Object);
            Assert.That(result, Is.EqualTo("world"));
        }
    }
}