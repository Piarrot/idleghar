using IdlegharDotnetBackend.Utils;
using IdlegharDotnetDomain.Constants;
using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetBackend.Providers
{
    public class CharactersProvider : ICharactersProvider
    {
        private Dictionary<string, Character> charactersById = new();
        private Dictionary<string, Character> charactersByPlayerId = new();

        public IRepositoryAggregator RepositoryAggregator { get; }

        public CharactersProvider(IRepositoryAggregator repositoryAggregator)
        {
            RepositoryAggregator = repositoryAggregator;
        }

        public async Task<List<Character>> FindAllQuesting()
        {
            await Task.Yield();
            return charactersById.Values.ToList().FindAll(c => c.IsQuesting);
        }

        public async Task Save(Character character)
        {
            await Task.Yield();
            var clonedCharacter = (Character)Hacks.TEMP_DO_NOT_USE_DeepClone(character)!;
            charactersById[character.Id] = clonedCharacter;
            charactersByPlayerId[character.Owner.Id] = clonedCharacter;
            await RepositoryAggregator.PlayersProvider.Save(character.Owner);
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
            return (Character?)Hacks.TEMP_DO_NOT_USE_DeepClone(character);
        }

        public async Task<Character?> FindByPlayerId(string playerId)
        {
            await Task.Yield();
            Character? character = null;
            charactersByPlayerId.TryGetValue(playerId, out character);
            return (Character?)Hacks.TEMP_DO_NOT_USE_DeepClone(character);
        }

        public async Task<Character> GetCharacterFromPlayerOrThrow(string playerId)
        {
            var character = await this.FindByPlayerId(playerId);
            if (character == null)
                throw new InvalidOperationException(ErrorMessages.CHARACTER_NOT_CREATED);

            return (Character)Hacks.TEMP_DO_NOT_USE_DeepClone(character)!;
        }
    }
}