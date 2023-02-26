using IdlegharDotnetBackend.Utils;
using IdlegharDotnetDomain.Constants;
using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetBackend.Providers
{
    public class PlayersProvider : IPlayersProvider
    {

        private Dictionary<string, Player> playersByEmail = new();
        private Dictionary<string, Player> playersByUsername = new();
        private Dictionary<string, Player> playersById = new();

        public PlayersProvider(IRepositoryAggregator repositoryAggregator)
        {
            RepositoryAggregator = repositoryAggregator;
        }

        private IRepositoryAggregator RepositoryAggregator { get; }

        public async Task<Player?> FindByEmail(string email)
        {
            await Task.Yield();
            Player? result = null;
            playersByEmail.TryGetValue(email, out result);
            return (Player?)Hacks.TEMP_DO_NOT_USE_DeepClone(result);
        }

        public async Task<Player?> FindById(string id)
        {
            await Task.Yield();
            Player? result = null;
            playersById.TryGetValue(id, out result);
            return (Player?)Hacks.TEMP_DO_NOT_USE_DeepClone(result);
        }

        public async Task<Player?> FindByUsername(string username)
        {
            await Task.Yield();
            Player? result = null;
            playersByUsername.TryGetValue(username, out result);
            return (Player?)Hacks.TEMP_DO_NOT_USE_DeepClone(result);
        }

        public async Task<PlayerCreds?> FindCredsFromEmail(string email)
        {
            await Task.Yield();
            Player? result = null;
            playersByEmail.TryGetValue(email, out result);
            if (result == null) return null;
            return PlayerCreds.From(result);
        }

        public async Task<Player> GetByIdOrThrow(string id)
        {
            var player = await FindById(id);
            if (player == null)
            {
                throw new ArgumentException(ErrorMessages.INVALID_PLAYER);
            }
            return player;
        }

        public async Task Save(Player player)
        {
            await Task.Yield();
            var clonedPlayer = (Player)Hacks.TEMP_DO_NOT_USE_DeepClone(player)!;
            playersByEmail[player.Email] = clonedPlayer;
            playersByUsername[player.Username] = clonedPlayer;
            playersById[player.Id] = clonedPlayer;

            if (player.Character != null)
                await RepositoryAggregator.CharactersProvider.Save(player.Character);
        }
    }
}