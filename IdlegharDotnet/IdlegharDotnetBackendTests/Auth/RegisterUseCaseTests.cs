namespace IdlegharDotnetBackendTests;

using IdlegharDotnetBackend;
using IdlegharDotnetShared;

public class RegisterUseCaseTests
{
    IUsersProvider usersProvider = new MockUsersProvider();
    ICryptoProvider hashProvider = new CryptoProvider();

    MockEmailsProvider emailsProvider = new MockEmailsProvider();

    [SetUp]
    public void Setup()
    {
        this.usersProvider = new MockUsersProvider();
    }

    [Test]
    public async Task RegistersAUserAndHashesThePassword()
    {
        var testInput = new RegisterUseCaseInput()
        {
            Email = "email@email.com",
            Password = "user1234",
            Username = "CoolUser69"
        };

        var useCase = new RegisterUseCase(usersProvider, hashProvider, emailsProvider);

        await useCase.Handle(testInput);
        var user = await this.usersProvider.FindUserByEmail(testInput.Email);
        Assert.NotNull(user);
        if (user == null) //Just so the compiler can infer that in the next line, user is not null
            return;
        Assert.IsTrue(hashProvider.DoesPasswordMatches(user.Password, testInput.Password));

        Assert.AreEqual(emailsProvider.CountEmailsSentTo(testInput.Email), 1);
        Assert.IsNotNull(emailsProvider.GetEmailsSentTo(testInput.Email)[0]?.Context?["code"]);
        Assert.IsNotNull(emailsProvider.GetEmailsSentTo(testInput.Email)[0]?.Context?["username"]);
    }

    [Test]
    public async Task FailsToRegisterAnEmailAlreadyRegistered()
    {
        await usersProvider.Save(new User()
        {
            Email = "emailAlreadyRegistered@email.com",
            Password = this.hashProvider.HashPassword("user1234"),
            Id = Guid.NewGuid().ToString()
        });

        var testInput = new RegisterUseCaseInput()
        {
            Email = "emailAlreadyRegistered@email.com",
            Password = "user1234",
            Username = "CoolUser69"
        };

        var useCase = new RegisterUseCase(usersProvider, hashProvider, emailsProvider);

        Assert.ThrowsAsync<EmailInUseException>(async delegate ()
        {
            await useCase.Handle(testInput);
        });
        Assert.AreEqual(emailsProvider.CountEmailsSentTo(testInput.Email), 0);
    }
}