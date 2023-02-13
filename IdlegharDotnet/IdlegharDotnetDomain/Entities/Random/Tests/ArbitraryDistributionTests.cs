using IdlegharDotnetDomain.Providers;
using IdlegharDotnetDomain.Tests;
using Moq;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Entities.Random.Tests
{
    public class RandomValueFromChancesTests : BaseTests
    {
        [Test]
        public void ItShouldProperlyResolveChances()
        {
            ArbitraryDistribution<string> valueFromChances = new()
            {
                ["hello"] = 0.6,
                ["banana"] = 0.3,
                ["world"] = 0.1
            };

            RandomnessProviderMock.SetupSequence(MockRandomDoubleLambda)
                .Returns(0.5)
                .Returns(0.7)
                .Returns(0.9);

            var result = valueFromChances.ResolveOne(RandomnessProviderMock.Object);
            Assert.That(result, Is.EqualTo("hello"));

            result = valueFromChances.ResolveOne(RandomnessProviderMock.Object);
            Assert.That(result, Is.EqualTo("banana"));

            result = valueFromChances.ResolveOne(RandomnessProviderMock.Object);
            Assert.That(result, Is.EqualTo("world"));
        }
    }
}