using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Tests;
using IdlegharDotnetShared.Auth;
using NUnit.Framework;

namespace IdlegharDotnetDomain.UseCases.Auth.Tests
{
    public class RegisterUseCaseTests : BaseTests
    {
        [Test]
        public async Task RegistersAPlayerAndHashesThePassword()
        {
            var testInput = new RegisterUseCaseRequest("email@email.com", "user1234", "CoolUser69");

            var useCase = new RegisterUseCase(PlayersProvider, CryptoProvider, EmailsProvider);

            await useCase.Handle(testInput);
            var player = await PlayersProvider.FindByEmail(testInput.Email);
            Assert.That(player, Is.Not.Null);
            Assert.That(CryptoProvider.DoesPasswordMatches(player!.Password, testInput.Password), Is.True);
            Assert.That(player.EmailValidated, Is.False);


            var codeSent = EmailsProvider.GetEmailsSentTo(testInput.Email)[0]?.Context?["code"];
            Assert.That(EmailsProvider.CountEmailsSentTo(testInput.Email), Is.EqualTo(1));
            Assert.That(codeSent, Is.Not.Null);
            Assert.That(EmailsProvider.GetEmailsSentTo(testInput.Email)[0]?.Context?["username"], Is.Not.Null);
            Assert.That(player.EmailValidationCode, Is.EqualTo(codeSent));
        }

        [Test]
        public async Task FailsToRegisterAnEmailAlreadyRegistered()
        {
            await PlayersProvider.Save(new Player()
            {
                Email = "emailAlreadyRegistered@email.com",
                Password = CryptoProvider.HashPassword("user1234"),
                Id = Guid.NewGuid().ToString(),
                Username = "CoolUser69"
            });

            var testInput = new RegisterUseCaseRequest("emailAlreadyRegistered@email.com", "user1234", "CoolUser69");

            var useCase = new RegisterUseCase(PlayersProvider, CryptoProvider, EmailsProvider);

            var ex = Assert.ThrowsAsync<ArgumentException>(async delegate ()
            {
                await useCase.Handle(testInput);
            });
            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.EMAIL_IN_USE));
            Assert.That(EmailsProvider.CountEmailsSentTo(testInput.Email), Is.EqualTo(0));
        }
    }
}