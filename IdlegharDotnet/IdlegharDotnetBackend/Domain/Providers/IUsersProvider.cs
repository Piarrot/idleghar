namespace IdlegharDotnetBackend;
public interface IUsersProvider
{
    Task<User?> FindUserByEmail(string email);
    Task Save(User user);
}