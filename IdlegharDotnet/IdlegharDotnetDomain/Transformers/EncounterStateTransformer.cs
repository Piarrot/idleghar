using IdlegharDotnetDomain.Entities.Encounters;
using IdlegharDotnetShared.Quests;

namespace IdlegharDotnetDomain.Transformers
{
    public class EncounterStateTransformer : Transformer<EncounterState, EncounterStateViewModel>
    {
        public override EncounterStateViewModel TransformOne(EncounterState entity)
        {
            return new()
            {
                Difficulty = entity.Encounter.Difficulty,
                Result = entity.Result,
                Events = entity.EncounterEvents.ToList(),
            };
        }
    }
}