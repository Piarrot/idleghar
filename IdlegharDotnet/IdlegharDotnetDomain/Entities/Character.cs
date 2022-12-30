using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdlegharDotnetDomain.Entities
{
    public class Character
    {
        public string Id { get; internal set; }
        public string Name { get; set; }
        public Quest CurrentQuest { get; set; }
    }
}