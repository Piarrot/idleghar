using IdlegharDotnetShared.SharedConstants;

namespace IdlegharDotnetShared.Characters
{
    public record class LevelUpCharacterUseCaseRequest(Dictionary<CharacterStat, int> attributeIncrease)
    {
    }
}