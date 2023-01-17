using IdlegharDotnetDomain.Entities;
using IdlegharDotnetShared.Quests;

namespace IdlegharDotnetDomain.Transformers
{
    public class QuestTransformer : Transformer<Quest, QuestViewModel>
    {
        public override QuestViewModel TransformOne(Quest quest)
        {
            return new QuestViewModel(quest.Id, quest.Name!, quest.Difficulty!);
        }
    }
}