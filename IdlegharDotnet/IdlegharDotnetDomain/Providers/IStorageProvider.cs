using IdlegharDotnetDomain.Entities;

namespace IdlegharDotnetDomain.Providers
{
    public interface IStorageProvider
    {
        Task<PlayerCreds?> FindPlayerCredsFromEmail(string email);

        //Players
        Task<Player?> FindPlayerByEmailOrUsername(string emailOrUsername);
        Task<Player?> FindPlayerById(string id);
        Task<Player> SavePlayer(Player player);
        Task<Player> GetPlayerByIdOrThrow(string id);

        //Characters
        Task<List<Character>> FindAllCharactersQuesting();
        Task<List<Character>> FindAllCharactersNotQuesting();
        Task<Character> SaveCharacter(Character character);
        Task<Character?> FindCharacterById(string id);
        Task<Character?> FindCharacterByPlayerId(string id);
        Task<Character> GetCharacterByPlayerIdOrThrow(string playerId);
    }
}