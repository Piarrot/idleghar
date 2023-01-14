using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.Tests.Factories
{
    public class FakeUserFactory
    {
        private ICryptoProvider CryptoProvider;
        private IUsersProvider UsersProvider;
        private FakeCharacterFactory FakeCharacterFactory;

        public FakeUserFactory(ICryptoProvider cryptoProvider, IUsersProvider usersProvider, FakeCharacterFactory fakeCharacterFactory)
        {
            CryptoProvider = cryptoProvider;
            UsersProvider = usersProvider;
            FakeCharacterFactory = fakeCharacterFactory;
        }

        public User CreateUser()
        {
            return CreateUser(Faker.Internet.Email(), "user1234", Faker.Internet.UserName(), true);
        }

        public User CreateUser(string email, string password, string username, bool emailValidated)
        {
            var user = new User
            {
                Email = email,
                EmailValidated = emailValidated,
                Username = username,
                Password = CryptoProvider.HashPassword(password)
            };
            return user;
        }

        public async Task<User> CreateAndStoreUser(string email, string password, string username, bool emailValidated)
        {
            var user = CreateUser(email, password, username, emailValidated);
            await UsersProvider.Save(user);
            return user;
        }

        public async Task<User> CreateAndStoreUser()
        {
            var user = CreateUser();
            await UsersProvider.Save(user);
            return user;
        }

        public async Task<User> CreateAndStoreUserAndCharacter()
        {
            var user = CreateUser();
            user.Character = await FakeCharacterFactory.CreateAndStoreCharacter();
            await UsersProvider.Save(user);
            return user;
        }

        public async Task<User> CreateAndStoreUserAndCharacterWithQuest()
        {
            var user = CreateUser();
            user.Character = await FakeCharacterFactory.CreateAndStoreCharacterWithQuest();
            await UsersProvider.Save(user);
            return user;
        }
    }
}