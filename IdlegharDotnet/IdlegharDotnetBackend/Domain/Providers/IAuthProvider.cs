namespace IdlegharDotnetBackend;

public interface IAuthProvider
{
    string GenerateToken(User user);
    string? ParseTokenEmail(string token);
}