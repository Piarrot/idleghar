using System.Linq.Expressions;
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
        protected Mock<IRandomnessProvider> RandomnessProviderMock = new();
        protected MockQuestsProvider QuestsProvider = new MockQuestsProvider();
        protected MockTimeProvider TimeProvider = new MockTimeProvider();
        protected MockCharactersProvider CharactersProvider = new();
        protected MockItemsProvider ItemsProvider = new();


        protected FakePlayerFactory FakePlayerFactory;
        protected FakeCharacterFactory FakeCharacterFactory;
        protected FakeQuestFactory FakeQuestFactory;
        protected FakeItemFactory FakeItemFactory;

        protected Expression<Func<IRandomnessProvider, int>> MockRandomIntLambda = (r) => r.GetRandomInt(It.IsAny<int>(), It.IsAny<int>());
        protected Expression<Func<IRandomnessProvider, double>> MockRandomDoubleLambda = (r) => r.GetRandomDouble(It.IsAny<double>(), It.IsAny<double>());

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


        protected void InitFakers()
        {
            RandomnessProviderMock.Setup(MockRandomIntLambda).Returns(1);
            RandomnessProviderMock.Setup(MockRandomDoubleLambda).Returns(0.1);
            FakeQuestFactory = new FakeQuestFactory(RandomnessProviderMock.Object, QuestsProvider, TimeProvider);
            FakeCharacterFactory = new FakeCharacterFactory(CharactersProvider, FakeQuestFactory);
            FakePlayerFactory = new FakePlayerFactory(CryptoProvider, PlayersProvider, FakeCharacterFactory);
            FakeItemFactory = new(ItemsProvider);
        }
    }
}