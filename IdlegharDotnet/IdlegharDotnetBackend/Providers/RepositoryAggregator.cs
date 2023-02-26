using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetBackend.Providers
{
    public class RepositoryAggregator : IRepositoryAggregator
    {

        public RepositoryAggregator()
        {
            PlayersProvider = new PlayersProvider(this);
            CharactersProvider = new CharactersProvider(this);
        }

        public IPlayersProvider PlayersProvider { get; }
        public ICharactersProvider CharactersProvider { get; }
    }
}