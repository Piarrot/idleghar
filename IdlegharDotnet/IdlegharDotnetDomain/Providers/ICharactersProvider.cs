using IdlegharDotnetDomain.Entities;

namespace IdlegharDotnetDomain.Providers
{
    public interface ICharactersProvider
    {
        Task<List<Character>> FindAll();
        Task<List<Character>> FindAllQuesting();
        Task Save(Character character);
    }
}