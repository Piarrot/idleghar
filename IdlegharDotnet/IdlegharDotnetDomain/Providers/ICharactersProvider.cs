using IdlegharDotnetDomain.Entities;

namespace IdlegharDotnetDomain.Providers
{
    public interface ICharactersProvider
    {
        Task<List<Character>> FindAllQuesting();
        Task<List<Character>> FindAllNotQuesting();
        Task Save(Character character);
        Task<Character?> FindById(string id);
        Task<Character?> FindByPlayerId(string id);
        Task<Character> GetCharacterFromPlayerOrThrow(Player player);
    }
}