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
            Assert.NotNull(user);
            if (user == null) //Just so the compiler can infer that in the next line, user is not null
                return;
            Assert.IsTrue(CryptoProvider.DoesPasswordMatches(user.Password, testInput.Password));
            Assert.IsFalse(user.EmailValidated);


            var codeSent = EmailsProvider.GetEmailsSentTo(testInput.Email)[0]?.Context?["code"];
            Assert.AreEqual(EmailsProvider.CountEmailsSentTo(testInput.Email), 1);
            Assert.IsNotNull(codeSent);
            Assert.IsNotNull(EmailsProvider.GetEmailsSentTo(testInput.Email)[0]?.Context?["username"]);
            Assert.AreEqual(codeSent, user.EmailValidationCode);
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
            Assert.AreEqual(EmailsProvider.CountEmailsSentTo(testInput.Email), 0);
        }
    }
}