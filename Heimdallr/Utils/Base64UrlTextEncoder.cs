using System;

namespace ZenProgramming.Heimdallr.Utils
{
    /// <summary>
    /// Encoder for base64 URL
    /// </summary>
    public static class Base64UrlTextEncoder
    {
        /// <summary>
        /// Executes encode of provided data
        /// </summary>
        /// <param name="data">Data</param>
        /// <returns>Returns encoded data</returns>
        public static string Encode(byte[] data)
        {
            //Validazione argomenti
            if (data == null)throw new ArgumentNullException(nameof(data));

            //Converto a base64 il dato
            string base64 = Convert.ToBase64String(data);

            //Applico i trimming dei valori e i replace
            string encoded = base64
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');

            //Ritorno il valore encoded
            return encoded;
        }

        /// <summary>
        /// Exeutes decoding of provided data
        /// </summary>
        /// <param name="base64Url">Data to decode in base64 format</param>
        /// <returns>Returns decoded data</returns>
        public static byte[] Decode(string base64Url)
        {
            //Validazione argomenti
            if (base64Url == null) throw new ArgumentNullException(nameof(base64Url));

            //Eseguo i replace
            var purged = base64Url
                .Replace('-', '+')
                .Replace('_', '/');

            //Padding con "="
            var padded = Pad(purged);

            //Convero la stringa da base64
            var fromBase64 = Convert.FromBase64String(padded);

            //Ritorno il valore
            return fromBase64;
        }

        /// <summary>
        /// Exeutes passing of provided string
        /// </summary>
        /// <param name="text">Value to pad</param>
        /// <returns>Returns padded data</returns>
        private static string Pad(string text)
        {
            //Validazione argomenti
            if (text == null) throw new ArgumentNullException(nameof(text));

            //Calcolo del padding di riferimento
            var padding = 3 - (text.Length + 3) % 4;

            //Se il valore è 0, ritorno la stringa
            if (padding == 0)
                return text;

            //Compongo una stringa 
            return text + new string('=', padding);
        }
    }
}
