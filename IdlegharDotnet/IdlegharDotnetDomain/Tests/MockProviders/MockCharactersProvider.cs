using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.Tests.MockProviders
{
    public class MockCharactersProvider : ICharactersProvider
    {
        private Dictionary<string, Character> charactersById = new();
        private Dictionary<string, Character> charactersByPlayerId = new();

        public async Task<List<Character>> FindAllQuesting()
        {
            await Task.Yield();
            return charactersById.Values.ToList().FindAll(c => c.IsQuesting);
        }

        public async Task Save(Character character)
        {
            await Task.Yield();
            var clonedCharacter = (Character)TestUtils.DeepClone(character);
            charactersById[character.Id] = clonedCharacter;
            charactersByPlayerId[character.Player.Id] = clonedCharacter;
        }

        public async Task<List<Character>> FindAllNotQuesting()
        {
            await Task.Yield();
            return charactersById.Values.ToList().FindAll(c => !c.IsQuesting);
        }

        public async Task<Character?> FindById(string id)
        {
            await Task.Yield();
            Character? character = null;
            charactersById.TryGetValue(id, out character);
            return character;
        }

        public async Task<Character?> FindByPlayerId(string playerId)
        {
            await Task.Yield();
            Character? character = null;
            charactersByPlayerId.TryGetValue(playerId, out character);
            return character;
        }

        public async Task<Character> GetCharacterFromPlayerOrThrow(string playerId)
        {
            var character = await this.FindByPlayerId(playerId);
            if (character == null)
                throw new InvalidOperationException(Constants.ErrorMessages.CHARACTER_NOT_CREATED);

            return character;
        }
    }
}