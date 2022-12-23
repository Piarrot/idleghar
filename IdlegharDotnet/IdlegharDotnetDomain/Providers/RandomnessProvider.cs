using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdlegharDotnetDomain.Providers
{
    public class RandomnessProvider : IRandomnessProvider
    {
        private Random rng = new Random();

        public int GetRandomInt(int min, int max)
        {
            return rng.Next(min, max + 1);
        }
    }
}