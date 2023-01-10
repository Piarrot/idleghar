using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Exceptions;
using IdlegharDotnetDomain.Tests;
using IdlegharDotnetShared.Auth;
using NUnit.Framework;

namespace IdlegharDotnetDomain.UseCases.Auth.Tests
{
    public class RegisterUseCaseTests : BaseTests
    {
        [Test]
        public async Task RegistersAUserAndHashesThePassword()
        {
            var testInput = new RegisterUseCaseRequest("email@email.com", "user1234", "CoolUser69");

            var useCase = new RegisterUseCase(UsersProvider, CryptoProvider, EmailsProvider);

            await useCase.Handle(testInput);
            var user = await UsersProvider.FindByEmail(testInput.Email);
            Assert.That(user, Is.Not.Null);
            Assert.That(CryptoProvider.DoesPasswordMatches(user!.Password, testInput.Password), Is.True);
            Assert.That(user.EmailValidated, Is.False);


            var codeSent = EmailsProvider.GetEmailsSentTo(testInput.Email)[0]?.Context?["code"];
            Assert.That(EmailsProvider.CountEmailsSentTo(testInput.Email), Is.EqualTo(1));
            Assert.That(codeSent, Is.Not.Null);
            Assert.That(EmailsProvider.GetEmailsSentTo(testInput.Email)[0]?.Context?["username"], Is.Not.Null);
            Assert.That(user.EmailValidationCode, Is.EqualTo(codeSent));
        }

        [Test]
        public async Task FailsToRegisterAnEmailAlreadyRegistered()
        {
            await UsersProvider.Save(new User()
            {
                Email = "emailAlreadyRegistered@email.com",
                Password = CryptoProvider.HashPassword("user1234"),
                Id = Guid.NewGuid().ToString(),
                Username = "CoolUser69"
            });

            var testInput = new RegisterUseCaseRequest("emailAlreadyRegistered@email.com", "user1234", "CoolUser69");

            var useCase = new RegisterUseCase(UsersProvider, CryptoProvider, EmailsProvider);

            Assert.ThrowsAsync<EmailInUseException>(async delegate ()
            {
                await useCase.Handle(testInput);
            });
            Assert.That(EmailsProvider.CountEmailsSentTo(testInput.Email), Is.EqualTo(0));
        }
    }
}