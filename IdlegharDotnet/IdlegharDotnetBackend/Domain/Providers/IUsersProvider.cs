namespace IdlegharDotnetBackend;
public interface IUsersProvider
{
    Task<User?> FindUserByEmail(string email);
    Task<User?> FindUserByUsername(string username);
    Task Save(User user);
}