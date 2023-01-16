using IdlegharDotnetDomain.Tests;
using IdlegharDotnetShared.Auth;
using NUnit.Framework;

namespace IdlegharDotnetDomain.UseCases.Auth.Tests
{
    public class ResendValidationEmailTests : BaseTests
    {
        const string email = "email@email.com";

        [Test]
        public async Task GivenAnEmailItShouldResendAValidationEmail()
        {

            var result = await new RegisterUseCase(PlayersProvider, CryptoProvider, EmailsProvider).Handle(
                new RegisterUseCaseRequest(email, "user1234", "cooluser")
            );

            await new ResendValidationEmailUseCase(PlayersProvider, EmailsProvider, CryptoProvider).Handle(
                new ResendValidationUseCaseRequest(email)
            );

            var sentEmails = EmailsProvider.GetEmailsSentTo(email);
            Assert.That(sentEmails.Count, Is.EqualTo(2));

            var firstEmail = sentEmails[0];
            var secondEmail = sentEmails[1];

            Assert.That(secondEmail.Context!["code"], Is.Not.Null);
            Assert.That(secondEmail.Context["code"], Is.Not.EqualTo(firstEmail.Context!["code"]));
        }

        [Test]
        public async Task GivenAnIncorrectEmailItShouldFail()
        {
            var result = await new RegisterUseCase(PlayersProvider, CryptoProvider, EmailsProvider).Handle(
                new RegisterUseCaseRequest(email, "user1234", "cooluser")
            );

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await new ResendValidationEmailUseCase(PlayersProvider, EmailsProvider, CryptoProvider).Handle(
                    new ResendValidationUseCaseRequest("something.else@email.com")
                );
            });
            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.INVALID_EMAIL));

            var sentEmails = EmailsProvider.GetEmailsSentTo(email);
            Assert.That(sentEmails.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GivenAnEmailAlreadyValidatedItShouldFail()
        {
            var result = await new RegisterUseCase(PlayersProvider, CryptoProvider, EmailsProvider).Handle(
                new RegisterUseCaseRequest(email, "user1234", "cooluser")
            );

            var player = await PlayersProvider.FindById(result.Id);
            player!.EmailValidated = true;
            await PlayersProvider.Save(player);

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await new ResendValidationEmailUseCase(PlayersProvider, EmailsProvider, CryptoProvider).Handle(
                    new ResendValidationUseCaseRequest(email)
                );
            });

            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.EMAIL_ALREADY_VALIDATED));

            var sentEmails = EmailsProvider.GetEmailsSentTo(email);
            Assert.That(sentEmails.Count, Is.EqualTo(1));
        }
    }
}