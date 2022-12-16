namespace IdlegharDotnetBackend;

public interface IAuthProvider
{
    Task<string> GenerateToken(User user);
    Task<User?> ParseToken(string token);
}