using System;
using System.Security.Cryptography;
using System.Text;

namespace ZenProgramming.Heimdallr.Utils
{
    /// <summary>
    /// Utilities for token
    /// </summary>
    public class TokenUtils
    {
        /// <summary>
        /// Get hash for provided input
        /// </summary>
        /// <param name="input">Input</param>
        /// <returns>Returns hash</returns>
        public static string GetHash(string input)
        {
            //Istanzio l'algoritmo SHA-256
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            //Recupero i bytes e compongo lphash
            byte[] byteValue = Encoding.UTF8.GetBytes(input);
            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            //Converto il tutto a 64-bit string
            return Convert.ToBase64String(byteHash);
        }

        /// <summary>
        /// Generates random client secret
        /// </summary>
        /// <returns>Returns client secret</returns>
        public static string GenerateRandomClientSecret()
        {
            //Genero dei bytes random
            var key = new byte[32];
            RandomNumberGenerator.Create().GetBytes(key);

            //Eseguo l'encoding base 64
            return Base64UrlTextEncoder.Encode(key);
        }

        /// <summary>
        /// Generate client secret using key
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>Returns client secret</returns>
        public static string GenerateClientSecret(string input)
        {
            //Validazione argomenti
            if (string.IsNullOrEmpty(input)) throw new ArgumentNullException(nameof(input));

            //Converto in bytes
            byte[] byteValue = Encoding.UTF8.GetBytes(input);

            //Eseguo l'encoding base 64
            return Base64UrlTextEncoder.Encode(byteValue);
        }

        /// <summary>
        /// Encode provided string in Base-64 string
        /// </summary>
        /// <param name="plainText">Plain text</param>
        /// <returns>Returns encoded string</returns>
        public static string Base64Encode(string plainText)
        {
            //Validazione argomenti
            if (string.IsNullOrEmpty(plainText))
                return string.Empty;

            //Eseguo la conversione
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}