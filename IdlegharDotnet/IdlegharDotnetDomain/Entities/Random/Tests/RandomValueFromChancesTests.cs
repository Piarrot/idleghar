using NUnit.Framework;

namespace IdlegharDotnetDomain.Entities.Random.Tests
{
    public class RandomValueFromChancesTests
    {
        class MockRandomnessProvider : Providers.IRandomnessProvider
        {
            public MockRandomnessProvider(double value)
            {
                Value = value;
            }

            private double Value;

            public double GetRandomDouble(double min, double max)
            {
                return Value;
            }

            public int GetRandomInt(int min, int max)
            {
                return 0;
            }

            public T Resolve<T>(RandomValue<T> rndValue)
            {
                return rndValue.Resolve(this);
            }
        }

        [Test]
        public void ItShouldProperlyResolveChances()
        {
            RandomValueFromChances<string> valueFromChances = new(new(){
                new ("hello", 0.6),
                new ("banana", 0.3),
                new ("world", 0.1)
            });

            var result = valueFromChances.Resolve(new MockRandomnessProvider(0.5));
            Assert.That(result, Is.EqualTo("hello"));

            result = valueFromChances.Resolve(new MockRandomnessProvider(0.7));
            Assert.That(result, Is.EqualTo("banana"));

            result = valueFromChances.Resolve(new MockRandomnessProvider(0.9));
            Assert.That(result, Is.EqualTo("world"));
        }
    }
}