using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.Tests.Factories
{
    public class FakePlayerFactory
    {
        private ICryptoProvider CryptoProvider;
        private IStorageProvider StorageProvider;
        private FakeCharacterFactory FakeCharacterFactory;


        public FakePlayerFactory(ICryptoProvider cryptoProvider, IStorageProvider playersProvider, FakeCharacterFactory fakeCharacterFactory)
        {
            CryptoProvider = cryptoProvider;
            StorageProvider = playersProvider;
            FakeCharacterFactory = fakeCharacterFactory;
        }

        public Player CreatePlayer()
        {
            return CreatePlayer(Faker.Internet.Email(), "user1234", Faker.Internet.UserName(), true);
        }

        public Player CreatePlayer(string email, string password, string username, bool emailValidated)
        {
            var player = new Player
            {
                Email = email,
                EmailValidated = emailValidated,
                Username = username,
                Password = CryptoProvider.HashPassword(password)
            };
            return player;
        }

        public async Task<Player> CreateAndStorePlayer(string email, string password, string username, bool emailValidated)
        {
            var player = CreatePlayer(email, password, username, emailValidated);
            await StorageProvider.SavePlayer(player);
            return player;
        }

        public async Task<Player> CreateAndStorePlayer()
        {
            var player = CreatePlayer();
            await StorageProvider.SavePlayer(player);
            return player;
        }

        public async Task<Player> CreateAndStorePlayerAndCharacter()
        {
            var player = CreatePlayer();
            player.Character = await FakeCharacterFactory.CreateAndStoreCharacter(player);
            await StorageProvider.SavePlayer(player);
            return player;
        }

        public async Task<Player> CreateAndStorePlayerAndCharacterWithQuest()
        {
            var player = CreatePlayer();
            player.Character = await FakeCharacterFactory.CreateAndStoreCharacterWithQuest(player);
            await StorageProvider.SavePlayer(player);
            return player;
        }
    }
}