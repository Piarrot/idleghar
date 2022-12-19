using IdlegharDotnetDomain;
namespace IdlegharDotnetDomainTests;
public class MockCryptoProvider : ICryptoProvider
{
    public bool DoesPasswordMatches(string hash, string plainPassword)
    {
        return hash == HashPassword(plainPassword);
    }

    public string GetRandomNumberDigits(int digitCount)
    {
        Random r = new Random();
        int[] randomNumbers = { r.Next(0, 9), r.Next(0, 9), r.Next(0, 9), r.Next(0, 9), r.Next(0, 9), r.Next(0, 9) };
        return String.Concat(randomNumbers);
    }

    public string HashPassword(string plainPassword)
    {
        return $"[HASHED]{plainPassword}[HASHED]";
    }
}