using IdlegharDotnetDomain.Exceptions;
using IdlegharDotnetDomain.UseCases.Auth;
using IdlegharDotnetShared.Auth;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Tests.UseCases.Auth
{
    public class ResendValidationEmailTests : BaseTests
    {
        const string email = "email@email.com";

        [Test]
        public async Task GivenAnEmailItShouldResendAValidationEmail()
        {

            var result = await new RegisterUseCase(UsersProvider, CryptoProvider, EmailsProvider).Handle(
                new RegisterUseCaseRequest(email, "user1234", "cooluser")
            );

            await new ResendValidationEmailUseCase(UsersProvider, EmailsProvider, CryptoProvider).Handle(
                new ResendValidationUseCaseRequest(email)
            );

            var sentEmails = EmailsProvider.GetEmailsSentTo(email);
            Assert.AreEqual(2, sentEmails.Count);

            var firstEmail = sentEmails[0];
            var secondEmail = sentEmails[1];

            Assert.NotNull(secondEmail.Context!["code"]);
            Assert.AreNotEqual(firstEmail.Context!["code"], secondEmail.Context["code"]);
        }

        [Test]
        public async Task GivenAnIncorrectEmailItShouldFail()
        {
            var result = await new RegisterUseCase(UsersProvider, CryptoProvider, EmailsProvider).Handle(
                new RegisterUseCaseRequest(email, "user1234", "cooluser")
            );

            Assert.ThrowsAsync<InvalidEmailException>(async () =>
            {
                await new ResendValidationEmailUseCase(UsersProvider, EmailsProvider, CryptoProvider).Handle(
                    new ResendValidationUseCaseRequest("something.else@email.com")
                );
            });

            var sentEmails = EmailsProvider.GetEmailsSentTo(email);
            Assert.AreEqual(1, sentEmails.Count);
        }

        [Test]
        public async Task GivenAnEmailAlreadyValidatedItShouldFail()
        {
            var result = await new RegisterUseCase(UsersProvider, CryptoProvider, EmailsProvider).Handle(
                new RegisterUseCaseRequest(email, "user1234", "cooluser")
            );

            var user = await UsersProvider.FindById(result.Id);
            user!.EmailValidated = true;
            await UsersProvider.Save(user);

            Assert.ThrowsAsync<InvalidEmailException>(async () =>
            {
                await new ResendValidationEmailUseCase(UsersProvider, EmailsProvider, CryptoProvider).Handle(
                    new ResendValidationUseCaseRequest(email)
                );
            });

            var sentEmails = EmailsProvider.GetEmailsSentTo(email);
            Assert.AreEqual(1, sentEmails.Count);
        }
    }
}