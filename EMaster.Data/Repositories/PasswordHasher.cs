using EMaster.Domain.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace EMaster.Data.Repositories
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 128 / 8;
        private const int HashSize = 256 / 8;
        private const int Iterations = 10000;
        private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;
        private const string Delimiter = ":";
        public string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

            return string.Join(Delimiter,Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        public bool Verify(string password, string passwordHash)
        {
            var parts = passwordHash.Split(Delimiter);
            var salt = Convert.FromBase64String(parts[0]);
            var hash = Convert.FromBase64String(parts[1]);
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);
            return CryptographicOperations.FixedTimeEquals(hash, hashToCompare);
        }
    }
}
