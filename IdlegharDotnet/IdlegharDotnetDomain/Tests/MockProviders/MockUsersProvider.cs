using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.Tests.MockProviders
{
    public class MockPlayersProvider : IPlayersProvider
    {
        private Dictionary<string, Player> playersByEmail = new Dictionary<string, Player>();
        private Dictionary<string, Player> playersByUsername = new Dictionary<string, Player>();
        private Dictionary<string, Player> playersById = new Dictionary<string, Player>();

        public async Task<Player?> FindByEmail(string email)
        {
            await Task.Yield();
            Player? result = null;
            playersByEmail.TryGetValue(email, out result);
            return result;
        }

        public async Task<Player?> FindById(string id)
        {
            await Task.Yield();
            Player? result = null;
            playersById.TryGetValue(id, out result);
            return result;
        }

        public async Task<Player?> FindByUsername(string username)
        {
            await Task.Yield();
            Player? result = null;
            playersByUsername.TryGetValue(username, out result);
            return result;
        }

        public async Task Save(Player player)
        {
            await Task.Yield();
            var clonedPlayer = (Player)TestUtils.DeepClone(player);
            playersByEmail[player.Email] = clonedPlayer;
            playersByUsername[player.Username] = clonedPlayer;
            playersById[player.Id] = clonedPlayer;
        }
    }
}