using IdlegharDotnetBackend;
using IdlegharDotnetShared;
namespace IdlegharDotnetBackendTests;

public class BaseTests
{
    protected IUsersProvider usersProvider = new MockUsersProvider();
    protected ICryptoProvider cryptoProvider = new CryptoProvider();
    protected IAuthProvider authProvider = new JWTProvider("Los gatitos son lo mejor");
    protected MockEmailsProvider emailsProvider = new MockEmailsProvider();

    [SetUp]
    public void Setup()
    {
        this.usersProvider = new MockUsersProvider();
    }
}