using IdlegharDotnetDomain.Entities;

namespace IdlegharDotnetDomain.Providers
{
    public interface IPlayersProvider
    {
        Task<Player?> FindByEmail(string email);
        Task<Player?> FindByUsername(string username);
        Task<Player?> FindById(string id);
        Task Save(Player player);
        Task<PlayerCreds?> FindCredsFromEmail(string email);
        Task<Player> GetByIdOrThrow(string id);
    }
}