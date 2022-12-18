using IdlegharDotnetBackend;
using IdlegharDotnetShared;
namespace IdlegharDotnetBackendTests;

public class LoginUseCaseTests
{
    IUsersProvider usersProvider = new MockUsersProvider();
    ICryptoProvider cryptoProvider = new CryptoProvider();
    IAuthProvider authProvider = new JWTProvider("Los gatitos son lo mejor 1234567");

    [SetUp]
    public void Setup()
    {
        this.usersProvider = new MockUsersProvider();
    }

    [Test]
    public async Task GivenCorrectCredentialsLogsInCorrectly()
    {
        var plainPassword = "user1234";
        var username = "CoolUser69";
        var email = "email@email.com";

        await this.usersProvider.Save(new User
        {
            Email = email,
            Id = Guid.NewGuid().ToString(),
            Username = username,
            Password = cryptoProvider.HashPassword(plainPassword)
        });

        var input = new LoginUseCaseRequest()
        {
            EmailOrUsername = username,
            Password = plainPassword
        };

        var useCase = new LoginUseCase(authProvider, usersProvider, cryptoProvider);
        var result = await useCase.Handle(input);

        Assert.IsInstanceOf(typeof(LoginUseCaseResponse), result);
        Assert.AreEqual(email, authProvider.ParseTokenEmail(result.Token));
    }

    [Test]
    public void GivenWrongUsernameFailsToLogin()
    {
        var plainPassword = "user1234";
        var username = "CoolUser69";

        var input = new LoginUseCaseRequest()
        {
            EmailOrUsername = username,
            Password = plainPassword
        };

        var useCase = new LoginUseCase(authProvider, usersProvider, cryptoProvider);
        Assert.ThrowsAsync(typeof(WrongCredentialsException), async () =>
        {
            await useCase.Handle(input);
        });
    }

    [Test]
    public async Task GivenWrongPasswordFailsToLogin()
    {
        var plainPassword = "user1234";
        var username = "CoolUser69";
        var email = "email@email.com";

        await this.usersProvider.Save(new User
        {
            Email = email,
            Id = Guid.NewGuid().ToString(),
            Username = username,
            Password = cryptoProvider.HashPassword(plainPassword)
        });

        var input = new LoginUseCaseRequest()
        {
            EmailOrUsername = username,
            Password = "aWrongPassword"
        };

        var useCase = new LoginUseCase(authProvider, usersProvider, cryptoProvider);
        Assert.ThrowsAsync(typeof(WrongCredentialsException), async () =>
        {
            await useCase.Handle(input);
        });
    }
}