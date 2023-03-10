using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Tests;
using IdlegharDotnetShared.Auth;
using NUnit.Framework;

namespace IdlegharDotnetDomain.UseCases.Auth.Tests
{
    public class LoginUseCaseTests : BaseTests
    {
        [Test]
        public async Task GivenCorrectCredentialsLogsInCorrectly()
        {
            var plainPassword = "user1234";
            var username = "CoolUser69";
            var email = "email@email.com";

            await this.StorageProvider.SavePlayer(new Player
            {
                Email = email,
                Id = Guid.NewGuid().ToString(),
                Username = username,
                EmailValidated = true,
                Password = CryptoProvider.HashPassword(plainPassword)
            });

            var input = new LoginUseCaseRequest(username, plainPassword);

            var useCase = new LoginUseCase(AuthProvider, StorageProvider, CryptoProvider);
            var result = await useCase.Handle(input);

            Assert.That(result, Is.InstanceOf<LoginUseCaseResponse>());
            Assert.That(AuthProvider.ParseTokenEmail(result.Token), Is.EqualTo(email));
        }

        [Test]
        public void GivenWrongUsernameFailsToLogin()
        {
            var plainPassword = "user1234";
            var username = "CoolUser69";

            var input = new LoginUseCaseRequest(username, plainPassword);

            var useCase = new LoginUseCase(AuthProvider, StorageProvider, CryptoProvider);
            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await useCase.Handle(input);
            });
            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.INVALID_CREDENTIALS));
        }

        [Test]
        public async Task GivenWrongPasswordFailsToLogin()
        {
            var plainPassword = "user1234";
            var username = "CoolUser69";
            var email = "email@email.com";

            await this.StorageProvider.SavePlayer(new Player
            {
                Email = email,
                Id = Guid.NewGuid().ToString(),
                Username = username,
                Password = CryptoProvider.HashPassword(plainPassword)
            });

            var input = new LoginUseCaseRequest(username, "wrongPassword");

            var useCase = new LoginUseCase(AuthProvider, StorageProvider, CryptoProvider);
            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await useCase.Handle(input);
            });
            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.INVALID_CREDENTIALS));
        }

        [Test]
        public async Task GivenAPlayerWithoutValidatedEmailFailsToLogin()
        {
            var plainPassword = "user1234";
            var username = "CoolUser69";
            var email = "email@email.com";

            await this.StorageProvider.SavePlayer(new Player
            {
                Email = email,
                Id = Guid.NewGuid().ToString(),
                Username = username,
                Password = CryptoProvider.HashPassword(plainPassword)
            });

            var input = new LoginUseCaseRequest(username, plainPassword);

            var useCase = new LoginUseCase(AuthProvider, StorageProvider, CryptoProvider);
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await useCase.Handle(input);
            });
            Assert.That(ex!.Message, Is.EqualTo(Constants.ErrorMessages.EMAIL_NOT_VALIDATED));
        }
    }
}