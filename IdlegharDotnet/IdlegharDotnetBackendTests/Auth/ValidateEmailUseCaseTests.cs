using IdlegharDotnetBackend;
using IdlegharDotnetShared;
namespace IdlegharDotnetBackendTests;

public class ValidateEmailUseCaseTests : BaseTests
{
    [Test]
    public async Task GivenCorrectValidationCodeShouldValidateEmail()
    {
        var email = "email@email.com";
        var registerUseCase = new RegisterUseCase(this.usersProvider, this.cryptoProvider, this.emailsProvider);
        var result = await registerUseCase.Handle(new RegisterUseCaseRequest
        {
            Email = email,
            Password = "user1234",
            Username = "CoolUser69"
        });

        var mails = this.emailsProvider.GetEmailsSentTo(email);
        var sentCode = mails[0].Context["code"];

        var validateUseCase = new ValidateEmailUseCase(this.usersProvider);
        await validateUseCase.Handle(new ValidateEmailUseCaseRequest
        {
            Id = result.Id,
            Code = sentCode
        });

        var user = await usersProvider.FindById(result.Id);

        Assert.IsTrue(user.EmailValidated);
        Assert.AreEqual(null, user.EmailValidationCode);
    }
}