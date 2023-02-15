using System.Collections;
using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.Entities.Random
{
    public class UniformDistribution<T> : RandomValue<T>, IEnumerable<T>
    {
        private List<T> items = new();

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        public override bool Matches(T value)
        {
            return items.Exists((i) => i!.Equals(value));
        }

        public override T ResolveOne(IRandomnessProvider randProvider)
        {
            var randValue = randProvider.GetRandomInt(0, items.Count - 1);
            return items[randValue];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }

        public void Add(T item)
        {
            items.Add(item);
        }
    }
}