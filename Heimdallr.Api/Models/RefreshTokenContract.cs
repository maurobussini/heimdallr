using System;

namespace ZenProgramming.Heimdallr.Api.Models
{
    /// <summary>
    /// Contract for refresh token
    /// </summary>
    public class RefreshTokenContract
    {
        /// <summary>
        /// Token hash
        /// </summary>
        public string TokenHash { get; set; }

        /// <summary>
        /// ClientdId
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Issue time in UTC format
        /// </summary>
        public DateTime IssuedUtc { get; set; }

        /// <summary>
        /// Expire time in UTC format
        /// </summary>
        public DateTime ExpiresUtc { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string ProtectedTicket { get; set; }
    }
}
