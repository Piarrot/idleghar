using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.Tests.MockProviders
{
    public class MockPlayersProvider : IStorageProvider
    {
        private Dictionary<string, Player> playersByEmail = new();
        private Dictionary<string, Player> playersByUsername = new();
        private Dictionary<string, Player> playersById = new();

        private Dictionary<string, Character> charactersById = new();

        public async Task<List<Character>> FindAllCharactersNotQuesting()
        {
            await Task.Yield();
            return charactersById.Values.ToList().FindAll(c => !c.IsQuesting);
        }

        public async Task<List<Character>> FindAllCharactersQuesting()
        {
            await Task.Yield();
            return charactersById.Values.ToList().FindAll(c => c.IsQuesting);
        }

        public async Task<Player?> FindPlayerByEmailOrUsername(string emailOrUsername)
        {
            await Task.Yield();
            Player? result = null;
            playersByEmail.TryGetValue(emailOrUsername, out result);
            if (result == null)
            {
                playersByUsername.TryGetValue(emailOrUsername, out result);
            }
            return (Player?)TestUtils.DeepClone(result);
        }

        public async Task<Player?> FindPlayerById(string id)
        {
            await Task.Yield();
            Player? result = null;
            playersById.TryGetValue(id, out result);
            return (Player?)TestUtils.DeepClone(result);
        }

        public async Task<Character?> FindCharacterById(string id)
        {
            await Task.Yield();
            Character? character = null;
            charactersById.TryGetValue(id, out character);
            return (Character?)TestUtils.DeepClone(character);
        }

        public async Task<Character?> FindCharacterByPlayerId(string id)
        {
            var player = await FindPlayerById(id);
            return (Character?)TestUtils.DeepClone(player?.Character);
        }

        public async Task<PlayerCreds?> FindPlayerCredsFromEmail(string email)
        {
            await Task.Yield();
            Player? result = null;
            playersByEmail.TryGetValue(email, out result);
            if (result == null) return null;
            return PlayerCreds.From(result);
        }

        public async Task<Player> GetPlayerByIdOrThrow(string id)
        {
            var player = await FindPlayerById(id);
            if (player == null)
            {
                throw new ArgumentException(Constants.ErrorMessages.INVALID_PLAYER);
            }
            return player;
        }

        public async Task<Character> GetCharacterByPlayerIdOrThrow(string playerId)
        {
            var player = await this.GetPlayerByIdOrThrow(playerId);
            return player.GetCharacterOrThrow();
        }

        public async Task<Player> SavePlayer(Player player)
        {
            await Task.Yield();
            var clonedPlayer = (Player)TestUtils.DeepClone(player)!;
            playersByEmail[player.Email] = clonedPlayer;
            playersByUsername[player.Username] = clonedPlayer;
            playersById[player.Id] = clonedPlayer;
            if (player.Character != null)
            {
                charactersById[clonedPlayer.Character!.Id] = clonedPlayer.Character;
            }
            return player;
        }

        public async Task<Character> SaveCharacter(Character character)
        {
            var player = await this.SavePlayer(character.Owner);
            return player.Character!;
        }

    }
}