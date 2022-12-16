using IdlegharDotnetBackend;
using IdlegharDotnetShared;

namespace IdlegharDotnetBackendTests;


public class LoginUseCaseTests
{
    IUsersProvider usersProvider = new MockUsersProvider();
    ICryptoProvider hashProvider = new CryptoProvider();

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task LogsInAUserWithCorrectCredentials()
    {
        var plainPassword = "user1234";
        var username = "CoolUser69";

        await this.usersProvider.Save(new User
        {
            Email = "email@email.com",
            Id = Guid.NewGuid().ToString(),
            Username = username,
            Password = hashProvider.HashPassword(plainPassword)
        });

        var input = new LoginUseCaseInput()
        {
            EmailOrUsername = username,
            Password = plainPassword
        };

        var useCase = new LoginUseCase();
        var result = await useCase.Handle(input);

        Assert.IsInstanceOf(typeof(LoginUseCaseOutput), result);
    }
}