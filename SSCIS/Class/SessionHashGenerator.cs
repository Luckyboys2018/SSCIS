using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace SSCIS.Class
{
    /// <summary>
    /// Random hash generator for session authentification
    /// </summary>
    public class SessionHashGenerator
    {
        /// <summary>
        /// Generates a random hash string 32 byte hash representation
        /// </summary>
        /// <returns>string hashcode</returns>
        public string GenerateHash()
        {
            var bytes = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

            return BitConverter.ToString(bytes);
        }
    }
}