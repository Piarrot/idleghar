using IdlegharDotnetDomain.Providers;
using IdlegharDotnetDomain.Tests.Factories;
using IdlegharDotnetDomain.Tests.MockProviders;
using Moq;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Tests
{
    public class BaseTests
    {
        protected MockPlayersProvider PlayersProvider = new MockPlayersProvider();
        protected MockCryptoProvider CryptoProvider = new MockCryptoProvider();
        protected MockAuthProvider AuthProvider = new MockAuthProvider();
        protected MockEmailsProvider EmailsProvider = new MockEmailsProvider();
        protected RandomnessProvider RandomnessProviderMock = new("pizza");
        protected MockQuestsProvider QuestsProvider = new MockQuestsProvider();
        protected MockTimeProvider TimeProvider = new MockTimeProvider();
        protected MockCharactersProvider CharactersProvider = new();
        protected MockItemsProvider ItemsProvider = new();


        protected FakePlayerFactory FakePlayerFactory;
        protected FakeCharacterFactory FakeCharacterFactory;
        protected FakeQuestFactory FakeQuestFactory;
        protected FakeItemFactory FakeItemFactory;

        public BaseTests()
        {
            InitFakers();
        }

        [SetUp]
        public void Setup()
        {
            PlayersProvider = new MockPlayersProvider();
            EmailsProvider = new MockEmailsProvider();

            InitFakers();
        }


        private void InitFakers()
        {
            FakeQuestFactory = new FakeQuestFactory(RandomnessProviderMock, QuestsProvider, TimeProvider);
            FakeCharacterFactory = new FakeCharacterFactory(CharactersProvider, FakeQuestFactory);
            FakePlayerFactory = new FakePlayerFactory(CryptoProvider, PlayersProvider, FakeCharacterFactory);
            FakeItemFactory = new(ItemsProvider);
        }
    }
}