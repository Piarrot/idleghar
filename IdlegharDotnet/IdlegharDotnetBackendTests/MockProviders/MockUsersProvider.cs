using IdlegharDotnetBackend;

public class MockUsersProvider : IUsersProvider
{
    private Dictionary<string, User> usersByEmail = new Dictionary<string, User>();

    public async Task<User?> FindUserByEmail(string email)
    {
        await Task.Yield();
        User? result = null;
        this.usersByEmail.TryGetValue(email, out result);
        return result;
    }

    public async Task Save(User user)
    {
        await Task.Yield();
        this.usersByEmail.Add(user.Email, user);
    }
}