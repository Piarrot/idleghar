using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdlegharDotnetDomain.Entities
{
    public class QuestBatch
    {
        public DateTime CreatedAt { get; set; }
        public List<Quest> Quests { get; set; }
    }
}