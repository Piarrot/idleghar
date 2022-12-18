using IdlegharDotnetBackend;

public class MockUsersProvider : IUsersProvider
{
    private Dictionary<string, User> usersByEmail = new Dictionary<string, User>();
    private Dictionary<string, User> usersByUsername = new Dictionary<string, User>();

    public async Task<User?> FindUserByEmail(string email)
    {
        await Task.Yield();
        User? result = null;
        this.usersByEmail.TryGetValue(email, out result);
        return result;
    }

    public async Task<User?> FindUserByUsername(string username)
    {
        await Task.Yield();
        User? result = null;
        this.usersByUsername.TryGetValue(username, out result);
        return result;
    }

    public async Task Save(User user)
    {
        await Task.Yield();
        this.usersByEmail.Add(user.Email, user);
        this.usersByUsername.Add(user.Username, user);
    }
}