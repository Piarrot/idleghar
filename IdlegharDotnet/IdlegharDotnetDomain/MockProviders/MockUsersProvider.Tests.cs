using IdlegharDotnetDomain;
namespace IdlegharDotnetDomainTests;
public class MockUsersProvider : IUsersProvider
{
    private Dictionary<string, User> usersByEmail = new Dictionary<string, User>();
    private Dictionary<string, User> usersByUsername = new Dictionary<string, User>();
    private Dictionary<string, User> usersById = new Dictionary<string, User>();

    public async Task<User?> FindByEmail(string email)
    {
        await Task.Yield();
        User? result = null;
        this.usersByEmail.TryGetValue(email, out result);
        return result;
    }

    public async Task<User?> FindById(string id)
    {
        await Task.Yield();
        User? result = null;
        this.usersById.TryGetValue(id, out result);
        return result;
    }

    public async Task<User?> FindByUsername(string username)
    {
        await Task.Yield();
        User? result = null;
        this.usersByUsername.TryGetValue(username, out result);
        return result;
    }

    public async Task Save(User user)
    {
        await Task.Yield();
        this.usersByEmail[user.Email] = user;
        this.usersByUsername[user.Username] = user;
        this.usersById[user.Id] = user;
    }
}