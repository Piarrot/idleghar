using IdlegharDotnetDomain.Entities;
using IdlegharDotnetShared.Quests;

namespace IdlegharDotnetDomain.Transformers
{
    public static class QuestTransformer
    {
        public static QuestDTO Transform(Quest quest)
        {
            return new QuestDTO(quest.Id, quest.Name!, quest.Difficulty!);
        }

        public static List<QuestDTO> Transform(List<Quest> quests)
        {
            return quests.ConvertAll<QuestDTO>((quest) =>
            {
                return Transform(quest);
            });
        }
    }
}