using IdlegharDotnetDomain.Providers;
using IdlegharDotnetDomain.Tests.Factories;
using IdlegharDotnetDomain.Tests.MockProviders;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Tests
{
    public class BaseTests
    {
        protected IUsersProvider UsersProvider = new MockUsersProvider();
        protected ICryptoProvider CryptoProvider = new MockCryptoProvider();
        protected IAuthProvider AuthProvider = new MockAuthProvider();
        protected MockEmailsProvider EmailsProvider = new MockEmailsProvider();
        protected IRandomnessProvider RandomnessProvider = new RandomnessProvider();
        protected IQuestsProvider QuestsProvider = new MockQuestsProvider();
        protected MockTimeProvider TimeProvider = new MockTimeProvider();


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
            FakeCharacterFactory = new FakeCharacterFactory(FakeQuestFactory);
            FakeUserFactory = new FakeUserFactory(CryptoProvider, UsersProvider, FakeCharacterFactory);
        }
    }
}