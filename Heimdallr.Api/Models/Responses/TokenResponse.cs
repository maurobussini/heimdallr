using System;
using Newtonsoft.Json;

namespace ZenProgramming.Heimdallr.Api.Models.Responses
{
    /// <summary>
    /// Represents response for generated token
    /// </summary>
    public class TokenResponse
    {
        /// <summary>
        /// Access token
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Refresh token
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// Expiration date
        /// </summary>
        [JsonProperty("expiration")]
        public DateTime Expiration { get; set; }

        /// <summary>
        /// Type of generated token
        /// </summary>
        [JsonProperty("token_type")]
        public string TokenType { get; set; } = "bearer";

        //public int expires_in { get; set; }
    }
}
