using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMaster.Domain.Interfaces
{
    public interface IPasswordHasher
    {
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
        bool Verify(string password, string hash);
    }
}
