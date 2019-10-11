using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BitcoinLoggerServer.Framework.Security
{
    public class EncryptionService
    {
        public string EncryptPassword(string password)
        {
            // SHA512 is disposable by inheritance.  
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                // Get the hashed string.  
                var res =  BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return res;
            }
        }       

    }
}
