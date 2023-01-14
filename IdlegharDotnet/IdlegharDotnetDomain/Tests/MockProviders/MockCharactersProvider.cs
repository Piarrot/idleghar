using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.Tests.MockProviders
{
    public class MockCharactersProvider : ICharactersProvider
    {
        private Dictionary<string, Character> charactersById = new();

        public async Task<List<Character>> FindAll()
        {
            await Task.Yield();
            return charactersById.Values.ToList();
        }

        public async Task<List<Character>> FindAllQuesting()
        {
            await Task.Yield();
            return charactersById.Values.ToList().FindAll(c => c.IsQuesting);
        }

        public async Task Save(Character character)
        {
            await Task.Yield();
            charactersById[character.Id] = (Character)TestUtils.DeepClone(character);
        }

        public async Task<List<Character>> FindAllNotQuesting()
        {
            await Task.Yield();
            return charactersById.Values.ToList().FindAll(c => !c.IsQuesting);
        }
    }
}