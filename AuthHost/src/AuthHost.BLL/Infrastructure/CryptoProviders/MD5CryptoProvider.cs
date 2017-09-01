using System;
using System.Security.Cryptography;
using System.Text;
using AuthHost.BLL.Interfaces;

namespace AuthHost.BLL.Infrastructure.CryptoProviders
{
    public class MD5CryptoProvider : ICryptoProvider
    {
        public string GetHash(string plaintext)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(plaintext));

                return Encoding.ASCII.GetString(result);
            }
        }

        public bool VerifyHash(string text, string hashedValue)
        {
            string newHashedValue = GetHash(text);

            var strcomparer = StringComparer.OrdinalIgnoreCase;

            return strcomparer.Compare(newHashedValue, hashedValue).Equals(0);
        }
    }
}