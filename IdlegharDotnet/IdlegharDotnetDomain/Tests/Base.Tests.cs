using IdlegharDotnetDomain.Providers;
using IdlegharDotnetDomain.Tests.MockProviders;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Tests
{
    public class BaseTests
    {
        protected IUsersProvider usersProvider = new MockUsersProvider();
        protected ICryptoProvider cryptoProvider = new MockCryptoProvider();
        protected IAuthProvider authProvider = new MockAuthProvider();
        protected MockEmailsProvider emailsProvider = new MockEmailsProvider();

        [SetUp]
        public void Setup()
        {
            usersProvider = new MockUsersProvider();
            emailsProvider = new MockEmailsProvider();
        }
    }
}