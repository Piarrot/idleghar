namespace IdlegharDotnetBackend;
public interface IUsersProvider
{
    Task<User?> FindByEmail(string email);
    Task<User?> FindByUsername(string username);
    Task<User?> FindById(string id);
    Task Save(User user);
}