using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace TideScraper.Core.Security
{
    public class HmacSha1SignatureGenerator : ISignatureGenerator
    {
        public string Generate(string secret, string data)
        {
            var hashAlgorithm = new HMACSHA1 { Key = Encoding.ASCII.GetBytes(secret) };
            byte[] dataBuffer = Encoding.ASCII.GetBytes(data);
            byte[] hashBytes = hashAlgorithm.ComputeHash(dataBuffer);

            return Convert.ToBase64String(hashBytes);
        }
    }
}
