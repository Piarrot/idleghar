using IdlegharDotnetDomain.Providers;
using IdlegharDotnetDomain.Tests;
using Moq;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Entities.Random.Tests
{
    public class ArbitraryPartialDistributionTests : BaseTests
    {
        [Test]
        public void ItShouldProperlyResolveChances()
        {
            ArbitraryPartialDistribution<string> valueFromChances = new()
            {
                ["hello"] = 0.2,
                ["banana"] = 0.2,
                ["world"] = 0.1
            };

            RandomnessProviderMock.SetupSequence(MockRandomDoubleLambda)
                .Returns(0.1)
                .Returns(0.35)
                .Returns(0.41)
                .Returns(0.8);

            var result = valueFromChances.ResolveOne(RandomnessProviderMock.Object);
            Assert.That(result.HasValue, Is.True);
            Assert.That(result.Value, Is.EqualTo("hello"));

            result = valueFromChances.ResolveOne(RandomnessProviderMock.Object);
            Assert.That(result.HasValue, Is.True);
            Assert.That(result.Value, Is.EqualTo("banana"));

            result = valueFromChances.ResolveOne(RandomnessProviderMock.Object);
            Assert.That(result.HasValue, Is.True);
            Assert.That(result.Value, Is.EqualTo("world"));

            result = valueFromChances.ResolveOne(RandomnessProviderMock.Object);
            Assert.That(result.HasValue, Is.False);
        }
    }
}