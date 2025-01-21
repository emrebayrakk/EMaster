using EMaster.Domain.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace EMaster.Data.Repositories
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly byte[] _key;
        private readonly byte[] _iv;

        public PasswordHasher()
        {
            _key = Encoding.UTF8.GetBytes("Your16CharKey123"); 
            _iv = Encoding.UTF8.GetBytes("Your16CharIV1234"); 
        }

        public string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));

            using (var aes = Aes.Create())
            {
                aes.Key = _key;
                aes.IV = _iv;

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var writer = new StreamWriter(cs))
                    {
                        writer.Write(plainText);
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException(nameof(cipherText));

            using (var aes = Aes.Create())
            {
                aes.Key = _key;
                aes.IV = _iv;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var reader = new StreamReader(cs))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public bool Verify(string password, string hash)
        {
            var decrypted = Decrypt(hash);
            return password == decrypted;
        }
    }
}
