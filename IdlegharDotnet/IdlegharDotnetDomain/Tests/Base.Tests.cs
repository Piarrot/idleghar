using IdlegharDotnetDomain;
using NUnit.Framework;
namespace IdlegharDotnetDomainTests;

public class BaseTests
{
    protected IUsersProvider usersProvider = new MockUsersProvider();
    protected ICryptoProvider cryptoProvider = new MockCryptoProvider();
    protected IAuthProvider authProvider = new MockAuthProvider();
    protected MockEmailsProvider emailsProvider = new MockEmailsProvider();

    [SetUp]
    public void Setup()
    {
        this.usersProvider = new MockUsersProvider();
    }
}