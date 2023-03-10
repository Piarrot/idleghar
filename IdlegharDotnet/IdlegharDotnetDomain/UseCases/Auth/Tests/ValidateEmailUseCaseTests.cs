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
            var registerUseCase = new RegisterUseCase(StorageProvider, CryptoProvider, EmailsProvider);
            var result = await registerUseCase.Handle(new RegisterUseCaseRequest(email, "user1234", "CoolUser69"));

            var mails = EmailsProvider.GetEmailsSentTo(email);
            var sentCode = mails[0].Context!["code"];

            var validateUseCase = new ValidateEmailUseCase(StorageProvider);
            await validateUseCase.Handle(new ValidateEmailUseCaseRequest(result.Id, sentCode));

            var player = await StorageProvider.GetPlayerByIdOrThrow(result.Id);

            Assert.That(player.EmailValidated, Is.True);
            Assert.That(player.EmailValidationCode, Is.Null);
        }

        [Test]
        public async Task GivenIncorrectValidationCodeShouldFailToValidateEmail()
        {
            var email = "email@email.com";
            var registerUseCase = new RegisterUseCase(StorageProvider, CryptoProvider, EmailsProvider);
            var result = await registerUseCase.Handle(new RegisterUseCaseRequest(email, "user1234", "CoolUser69"));

            var validateUseCase = new ValidateEmailUseCase(StorageProvider);

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await validateUseCase.Handle(new ValidateEmailUseCaseRequest(result.Id, "banana"));
            });
            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.INVALID_VALIDATION_CODE));

            var player = await StorageProvider.GetPlayerByIdOrThrow(result.Id);
            Assert.That(player.EmailValidated, Is.False);
        }

        [Test]
        public async Task GivenIncorrectPlayerIdShouldFailToValidateEmail()
        {
            var email = "email@email.com";
            var registerUseCase = new RegisterUseCase(StorageProvider, CryptoProvider, EmailsProvider);
            var result = await registerUseCase.Handle(new RegisterUseCaseRequest(email, "user1234", "CoolUser69"));

            var validateUseCase = new ValidateEmailUseCase(StorageProvider);

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await validateUseCase.Handle(new ValidateEmailUseCaseRequest("any-id", "any-code"));
            });
            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.INVALID_PLAYER));
        }

        [Test]
        public async Task GivenEmailAlreadyValidatedItShouldFail()
        {
            var player = await FakePlayerFactory.CreateAndStorePlayer();

            var validateUseCase = new ValidateEmailUseCase(StorageProvider);

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await validateUseCase.Handle(new ValidateEmailUseCaseRequest(player.Id, "any-code"));
            });
            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.EMAIL_ALREADY_VALIDATED));
        }
    }
}