using IdlegharDotnetDomain.Tests;
using IdlegharDotnetShared.Auth;
using NUnit.Framework;

namespace IdlegharDotnetDomain.UseCases.Auth.Tests
{
    public class ValidateEmailUseCaseTests : BaseTests
    {
        [Test]
        public async Task GivenCorrectValidationCodeShouldValidateEmail()
        {
            var email = "email@email.com";
            var registerUseCase = new RegisterUseCase(UsersProvider, CryptoProvider, EmailsProvider);
            var result = await registerUseCase.Handle(new RegisterUseCaseRequest(email, "user1234", "CoolUser69"));

            var mails = EmailsProvider.GetEmailsSentTo(email);
            var sentCode = mails[0].Context!["code"];

            var validateUseCase = new ValidateEmailUseCase(UsersProvider);
            await validateUseCase.Handle(new ValidateEmailUseCaseRequest(result.Id, sentCode));

            var user = await UsersProvider.FindById(result.Id);

            Assert.That(user!.EmailValidated, Is.True);
            Assert.That(user.EmailValidationCode, Is.Null);
        }

        [Test]
        public async Task GivenIncorrectValidationCodeShouldFailToValidateEmail()
        {
            var email = "email@email.com";
            var registerUseCase = new RegisterUseCase(UsersProvider, CryptoProvider, EmailsProvider);
            var result = await registerUseCase.Handle(new RegisterUseCaseRequest(email, "user1234", "CoolUser69"));

            var validateUseCase = new ValidateEmailUseCase(UsersProvider);

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await validateUseCase.Handle(new ValidateEmailUseCaseRequest(result.Id, "banana"));
            });
            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.INVALID_VALIDATION_CODE));

            var user = await UsersProvider.FindById(result.Id);
            Assert.That(user!.EmailValidated, Is.False);
        }

        [Test]
        public async Task GivenIncorrectUserIdShouldFailToValidateEmail()
        {
            var email = "email@email.com";
            var registerUseCase = new RegisterUseCase(UsersProvider, CryptoProvider, EmailsProvider);
            var result = await registerUseCase.Handle(new RegisterUseCaseRequest(email, "user1234", "CoolUser69"));

            var validateUseCase = new ValidateEmailUseCase(UsersProvider);

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await validateUseCase.Handle(new ValidateEmailUseCaseRequest("any-id", "any-code"));
            });
            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.INVALID_USER));
        }

        [Test]
        public async Task GivenEmailAlreadyValidatedItShouldFail()
        {
            var user = await FakeUserFactory.CreateAndStoreUser();

            var validateUseCase = new ValidateEmailUseCase(UsersProvider);

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await validateUseCase.Handle(new ValidateEmailUseCaseRequest(user.Id, "any-code"));
            });
            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.EMAIL_ALREADY_VALIDATED));
        }
    }
}