using System;
using System.ComponentModel.DataAnnotations;

namespace ZenProgramming.Heimdallr.Api.Models.Requests
{
    /// <summary>
    /// Request for update single audience
    /// </summary>
    public class UpdateAudienceRequest
    {
        /// <summary>
        /// Audience client identifier
        /// </summary>
        [Required]
        [StringLength(255)]
        public string ClientId { get; set; }

        /// <summary>
        /// Name of audience
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// Is native (confidential)
        /// </summary>
        [Required]
        public bool IsNative { get; set; }

        /// <summary>
        /// Is enabled
        /// </summary>
        [Required]
        public virtual bool IsEnabled { get; set; }

        /// <summary>
        /// Refresh token life time (in minutes)
        /// </summary>
        [Required]
        [Range(10, Int32.MaxValue)]
        public int RefreshTokenLifeTime { get; set; }

        /// <summary>
        /// Allowed origin
        /// </summary>
        [StringLength(255)]
        public string AllowedOrigin { get; set; }

        /// <summary>
        /// Has administrative access
        /// </summary>
        public bool HasAdministrativeAccess { get; set; }
    }
}
