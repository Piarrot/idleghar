using IdlegharDotnetDomain.UseCases.Auth;
using IdlegharDotnetShared.Auth;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Tests.UseCases.Auth
{
    public class ValidateEmailUseCaseTests : BaseTests
    {
        [Test]
        public async Task GivenCorrectValidationCodeShouldValidateEmail()
        {
            var email = "email@email.com";
            var registerUseCase = new RegisterUseCase(usersProvider, cryptoProvider, emailsProvider);
            var result = await registerUseCase.Handle(new RegisterUseCaseRequest
            {
                Email = email,
                Password = "user1234",
                Username = "CoolUser69"
            });

            var mails = emailsProvider.GetEmailsSentTo(email);
            var sentCode = mails[0].Context["code"];

            var validateUseCase = new ValidateEmailUseCase(usersProvider);
            await validateUseCase.Handle(new ValidateEmailUseCaseRequest
            {
                Id = result.Id,
                Code = sentCode
            });

            var user = await usersProvider.FindById(result.Id);

            Assert.IsTrue(user.EmailValidated);
            Assert.AreEqual(null, user.EmailValidationCode);
        }

        [Test]
        public async Task GivenIncorrectValidationCodeShouldFailToValidateEmail()
        {
            var email = "email@email.com";
            var registerUseCase = new RegisterUseCase(usersProvider, cryptoProvider, emailsProvider);
            var result = await registerUseCase.Handle(new RegisterUseCaseRequest
            {
                Email = email,
                Password = "user1234",
                Username = "CoolUser69"
            });

            var validateUseCase = new ValidateEmailUseCase(usersProvider);

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await validateUseCase.Handle(new ValidateEmailUseCaseRequest
                {
                    Id = result.Id,
                    Code = "banana"
                });
            });

            var user = await usersProvider.FindById(result.Id);
            Assert.IsFalse(user.EmailValidated);
        }

        [Test]
        public async Task GivenIncorrectUserIdShouldFailToValidateEmail()
        {
            var email = "email@email.com";
            var registerUseCase = new RegisterUseCase(usersProvider, cryptoProvider, emailsProvider);
            var result = await registerUseCase.Handle(new RegisterUseCaseRequest
            {
                Email = email,
                Password = "user1234",
                Username = "CoolUser69"
            });

            var validateUseCase = new ValidateEmailUseCase(usersProvider);

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await validateUseCase.Handle(new ValidateEmailUseCaseRequest
                {
                    Id = "any-id",
                    Code = "any-code"
                });
            });
        }
    }
}