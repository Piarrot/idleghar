using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdlegharDotnetDomain.Providers
{
    public interface IRandomnessProvider
    {
        int GetRandomInt(int min, int max);
    }
}