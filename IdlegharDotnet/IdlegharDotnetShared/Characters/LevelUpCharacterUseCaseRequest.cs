namespace IdlegharDotnetShared.Characters
{
    public record class LevelUpCharacterUseCaseRequest(Dictionary<Constants.Characters.Stat, int> attributeIncrease)
    {
    }
}