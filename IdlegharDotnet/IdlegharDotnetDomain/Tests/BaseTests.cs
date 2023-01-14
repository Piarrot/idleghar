using IdlegharDotnetDomain.Providers;
using IdlegharDotnetDomain.Tests.Factories;
using IdlegharDotnetDomain.Tests.MockProviders;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Tests
{
    public class BaseTests
    {
        protected MockUsersProvider UsersProvider = new MockUsersProvider();
        protected MockCryptoProvider CryptoProvider = new MockCryptoProvider();
        protected MockAuthProvider AuthProvider = new MockAuthProvider();
        protected MockEmailsProvider EmailsProvider = new MockEmailsProvider();
        protected RandomnessProvider RandomnessProvider = new RandomnessProvider("pizza");
        protected MockQuestsProvider QuestsProvider = new MockQuestsProvider();
        protected MockTimeProvider TimeProvider = new MockTimeProvider();
        protected MockCharactersProvider CharactersProvider = new();


        protected FakeUserFactory FakeUserFactory;
        protected FakeCharacterFactory FakeCharacterFactory;
        protected FakeQuestFactory FakeQuestFactory;

        public BaseTests()
        {
            InitFakers();
        }

        [SetUp]
        public void Setup()
        {
            UsersProvider = new MockUsersProvider();
            EmailsProvider = new MockEmailsProvider();

            InitFakers();
        }


        private void InitFakers()
        {
            FakeQuestFactory = new FakeQuestFactory(RandomnessProvider, QuestsProvider, TimeProvider);
            FakeCharacterFactory = new FakeCharacterFactory(CharactersProvider, FakeQuestFactory);
            FakeUserFactory = new FakeUserFactory(CryptoProvider, UsersProvider, FakeCharacterFactory);
        }
    }
}