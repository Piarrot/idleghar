using IdlegharDotnetDomain;
using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.Tests.MockProviders
{
    public class MockUsersProvider : IUsersProvider
    {
        private Dictionary<string, User> usersByEmail = new Dictionary<string, User>();
        private Dictionary<string, User> usersByUsername = new Dictionary<string, User>();
        private Dictionary<string, User> usersById = new Dictionary<string, User>();

        public async Task<User?> FindByEmail(string email)
        {
            await Task.Yield();
            User? result = null;
            usersByEmail.TryGetValue(email, out result);
            return result;
        }

        public async Task<User?> FindById(string id)
        {
            await Task.Yield();
            User? result = null;
            usersById.TryGetValue(id, out result);
            return result;
        }

        public async Task<User?> FindByUsername(string username)
        {
            await Task.Yield();
            User? result = null;
            usersByUsername.TryGetValue(username, out result);
            return result;
        }

        public async Task Save(User user)
        {
            await Task.Yield();
            usersByEmail[user.Email] = user;
            usersByUsername[user.Username] = user;
            usersById[user.Id] = user;
        }
    }
}