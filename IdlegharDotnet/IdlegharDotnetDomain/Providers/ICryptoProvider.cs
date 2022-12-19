namespace IdlegharDotnetDomain;
public interface ICryptoProvider
{
    string HashPassword(string plainPassword);
    bool DoesPasswordMatches(string hash, string plainPassword);
    string GetRandomNumberDigits(int digitCount);
}