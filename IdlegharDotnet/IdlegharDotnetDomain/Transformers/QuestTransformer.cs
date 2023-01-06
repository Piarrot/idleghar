using IdlegharDotnetDomain.Entities;
using IdlegharDotnetShared.Quests;

namespace IdlegharDotnetDomain.Transformers
{
    public static class QuestTransformer
    {
        public static QuestViewModel Transform(Quest quest)
        {
            return new QuestViewModel(quest.Id, quest.Name!, quest.Difficulty!);
        }

        public static List<QuestViewModel> Transform(List<Quest> quests)
        {
            return quests.ConvertAll<QuestViewModel>((quest) =>
            {
                return Transform(quest);
            });
        }
    }
}