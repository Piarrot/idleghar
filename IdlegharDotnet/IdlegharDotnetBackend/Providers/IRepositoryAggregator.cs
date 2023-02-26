using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetBackend.Providers
{
    public interface IRepositoryAggregator
    {
        public ICharactersProvider CharactersProvider { get; }
        public IPlayersProvider PlayersProvider { get; }
    }
}