using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;
using IdlegharDotnetDomain.Tests.Factories;
using IdlegharDotnetDomain.Tests.MockProviders;
using NUnit.Framework;

namespace IdlegharDotnetDomain.Tests
{
    public class BaseTests
    {
        protected IUsersProvider UsersProvider = new MockUsersProvider();
        protected ICryptoProvider CryptoProvider = new MockCryptoProvider();
        protected IAuthProvider AuthProvider = new MockAuthProvider();
        protected MockEmailsProvider EmailsProvider = new MockEmailsProvider();
        protected IRandomnessProvider RandomnessProvider = new RandomnessProvider();
        protected IQuestsProvider QuestsProvider = new MockQuestsProvider();
        protected MockTimeProvider TimeProvider = new MockTimeProvider();

        [SetUp]
        public void Setup()
        {
            UsersProvider = new MockUsersProvider();
            EmailsProvider = new MockEmailsProvider();
        }

        protected async Task<User> CreateAndStoreUser(UserFactoryOptions? opts = null)
        {
            if (opts == null)
            {
                opts = new UserFactoryOptions();
            }

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = opts.Email,
                EmailValidated = opts.EmailValidated,
                Username = opts.Username,
                Password = CryptoProvider.HashPassword(opts.Password),
                Character = opts.Character
            };
            await UsersProvider.Save(user);
            return user;
        }

        protected Character CreateCharacter(string? name = null)
        {
            return new Character
            {
                Id = Guid.NewGuid().ToString(),
                Name = name ?? "CoolCharacter"
            };
        }
    }
}