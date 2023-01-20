using IdlegharDotnetDomain.Entities.Quests;
using IdlegharDotnetShared.Quests;

namespace IdlegharDotnetDomain.Transformers
{
    public class QuestStateTransformer : Transformer<QuestState, QuestStateViewModel>
    {
        QuestTransformer questTransformer = new();
        EncounterStateTransformer encounterStateTransformer = new();

        public override QuestStateViewModel TransformOne(QuestState entity)
        {
            return new QuestStateViewModel(questTransformer.TransformOne(entity.Quest))
            {
                Progress = entity.Progress,
                PreviousEncounters = encounterStateTransformer.TransformMany(entity.Previous),
            };
        }
    }
}