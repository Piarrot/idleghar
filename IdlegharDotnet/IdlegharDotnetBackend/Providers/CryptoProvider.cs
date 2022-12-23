using System.Security.Cryptography;
using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetBackend.Providers
{
    public class CryptoProvider : ICryptoProvider
    {
        public bool DoesPasswordMatches(string hashedPassword, string plainPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = HashDeriveBytes(plainPassword, salt);
            byte[] hash = pbkdf2.GetBytes(20);
            return hash.SequenceEqual(hashBytes.Skip(salt.Count()));
        }

        public string GetRandomNumberDigits(int digitCount)
        {
            byte[] value = new byte[digitCount];
            RandomNumberGenerator.Create().GetBytes(value);
            string result = "";
            foreach (var bt in value)
            {
                result += bt.ToString()[0];
            }
            return result;
        }

        public string HashPassword(string plainPassword)
        {
            byte[] salt = new byte[16];
            RandomNumberGenerator.Create().GetBytes(salt);
            var pbkdf2 = HashDeriveBytes(plainPassword, salt);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }

        private Rfc2898DeriveBytes HashDeriveBytes(string plainPassword, byte[] salt)
        {
            return new Rfc2898DeriveBytes(plainPassword, salt, 10000, HashAlgorithmName.SHA256);
        }
    }
}