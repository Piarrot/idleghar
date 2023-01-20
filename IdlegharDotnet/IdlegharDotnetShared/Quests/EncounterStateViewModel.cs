using IdlegharDotnetShared.Constants;
using IdlegharDotnetShared.Events;

namespace IdlegharDotnetShared.Quests
{
    public class EncounterStateViewModel
    {
        public Difficulty Difficulty { get; set; }
        public EncounterResult Result { get; set; }
        public List<EncounterEvent> Events { get; set; } = new();
    }
}