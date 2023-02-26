using IdlegharDotnetShared.Events;
using IdlegharDotnetShared.SharedConstants;

namespace IdlegharDotnetShared.Quests
{
    public class EncounterStateViewModel
    {
        public Difficulty Difficulty { get; set; }
        public EncounterResult Result { get; set; }
        public List<EncounterEvent> Events { get; set; } = new();
    }
}