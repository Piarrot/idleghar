using IdlegharDotnetDomain.Entities;
using IdlegharDotnetShared.Characters;

namespace IdlegharDotnetDomain.Transformers
{
    public class CharacterTransformer : Transformer<Character, CharacterViewModel>
    {
        QuestStateTransformer questStateTransformer = new();

        public override CharacterViewModel TransformOne(Character entity)
        {
            return new CharacterViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Level = entity.Level,
                XP = entity.XP,
                MaxHP = entity.MaxHP,
                HP = entity.HP,
                Damage = entity.Damage,
                Toughness = entity.Toughness,
                CurrentQuestState = questStateTransformer.TransformOneOptional(entity.CurrentQuestState)
            };
        }
    }
}