using System.ComponentModel.DataAnnotations;

namespace ZenProgramming.Heimdallr.Api.Models.Requests
{
    /// <summary>
    /// Request for token
    /// </summary>
    public class TokenRequest
    {
        /// <summary>
        /// Client id
        /// </summary>
        [Required]
        [StringLength(255)]
        public string ClientId { get; set; }

        /// <summary>
        /// Grant type
        /// </summary>
        [Required]
        [StringLength(255)]
        public string GrantType { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        [StringLength(255)]
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [StringLength(255)]
        public string Password { get; set; }

        /// <summary>
        /// Refresh token
        /// </summary>
        [StringLength(255)]
        public string RefreshToken { get; set; }
    }
}