using IdlegharDotnetDomain.Exceptions;
using IdlegharDotnetDomain.UseCases.Auth;
using IdlegharDotnetShared.Auth;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Tests.UseCases.Auth
{
    public class RegisterUseCaseTests : BaseTests
    {
        [Test]
        public async Task RegistersAUserAndHashesThePassword()
        {
            var testInput = new RegisterUseCaseRequest()
            {
                Email = "email@email.com",
                Password = "user1234",
                Username = "CoolUser69"
            };

            var useCase = new RegisterUseCase(usersProvider, cryptoProvider, emailsProvider);

            await useCase.Handle(testInput);
            var user = await usersProvider.FindByEmail(testInput.Email);
            Assert.NotNull(user);
            if (user == null) //Just so the compiler can infer that in the next line, user is not null
                return;
            Assert.IsTrue(cryptoProvider.DoesPasswordMatches(user.Password, testInput.Password));
            Assert.IsFalse(user.EmailValidated);


            var codeSent = emailsProvider.GetEmailsSentTo(testInput.Email)[0]?.Context?["code"];
            Assert.AreEqual(emailsProvider.CountEmailsSentTo(testInput.Email), 1);
            Assert.IsNotNull(codeSent);
            Assert.IsNotNull(emailsProvider.GetEmailsSentTo(testInput.Email)[0]?.Context?["username"]);
            Assert.AreEqual(codeSent, user.EmailValidationCode);
        }

        [Test]
        public async Task FailsToRegisterAnEmailAlreadyRegistered()
        {
            await usersProvider.Save(new User()
            {
                Email = "emailAlreadyRegistered@email.com",
                Password = cryptoProvider.HashPassword("user1234"),
                Id = Guid.NewGuid().ToString(),
                Username = "CoolUser69"
            });

            var testInput = new RegisterUseCaseRequest()
            {
                Email = "emailAlreadyRegistered@email.com",
                Password = "user1234",
                Username = "CoolUser69"
            };

            var useCase = new RegisterUseCase(usersProvider, cryptoProvider, emailsProvider);

            Assert.ThrowsAsync<EmailInUseException>(async delegate ()
            {
                await useCase.Handle(testInput);
            });
            Assert.AreEqual(emailsProvider.CountEmailsSentTo(testInput.Email), 0);
        }
    }
}