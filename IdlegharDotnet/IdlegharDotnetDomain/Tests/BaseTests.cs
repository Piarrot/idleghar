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
        protected MockPlayersProvider StorageProvider = new MockPlayersProvider();
        protected MockCryptoProvider CryptoProvider = new MockCryptoProvider();
        protected MockAuthProvider AuthProvider = new MockAuthProvider();
        protected MockEmailsProvider EmailsProvider = new MockEmailsProvider();
        protected Mock<MockRandomnessProvider> RandomnessProviderMock = new();
        protected MockQuestsProvider QuestsProvider = new MockQuestsProvider();
        protected MockTimeProvider TimeProvider = new MockTimeProvider();


        protected FakePlayerFactory FakePlayerFactory;
        protected FakeCharacterFactory FakeCharacterFactory;
        protected FakeQuestFactory FakeQuestFactory;
        protected FakeItemFactory FakeItemFactory;

        protected Expression<Func<MockRandomnessProvider, int>> MockRandomIntLambda = (r) => r.GetRandomInt(It.IsAny<int>(), It.IsAny<int>());
        protected Expression<Func<MockRandomnessProvider, double>> MockRandomDoubleLambda = (r) => r.GetRandomDouble(It.IsAny<double>(), It.IsAny<double>());

        public BaseTests()
        {
            this.InitFakers();
        }

        [SetUp]
        public void Setup()
        {
            StorageProvider = new MockPlayersProvider();
            EmailsProvider = new MockEmailsProvider();

            InitFakers();
        }


        protected void InitFakers()
        {
            RandomnessProviderMock.CallBase = true;
            // RandomnessProviderMock.Setup(MockRandomIntLambda).Returns(1);
            // RandomnessProviderMock.Setup(MockRandomDoubleLambda).Returns(0.1);

            FakeQuestFactory = new FakeQuestFactory(RandomnessProviderMock.Object, QuestsProvider, TimeProvider);
            FakeCharacterFactory = new FakeCharacterFactory(StorageProvider, FakeQuestFactory);
            FakePlayerFactory = new FakePlayerFactory(CryptoProvider, StorageProvider, FakeCharacterFactory);
            FakeItemFactory = new();
        }
    }
}