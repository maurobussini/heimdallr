using System.ComponentModel.DataAnnotations;
using ZenProgramming.Chakra.Core.Entities;

namespace ZenProgramming.Heimdallr.Entities
{
    /// <summary>
    /// Represents audience (client app)
    /// </summary>
    public class Audience: ModernEntityBase
    {
        /// <summary>
        /// Name
        /// </summary>
        [Required]
        [StringLength(255)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Client id
        /// </summary>
        [Required]
        [StringLength(255)]
        public virtual string ClientId { get; set; }

        /// <summary>
        /// Client secret
        /// </summary>
        [Required]
        [StringLength(255)]
        public virtual string ClientSecret { get; set; }

        /// <summary>
        /// Is native application
        /// </summary>
        [Required]
        public virtual bool IsNative { get; set; }

        /// <summary>
        /// Is enabled
        /// </summary>
        [Required]
        public virtual bool IsEnabled { get; set; }

        /// <summary>
        /// Refresh token life time (in minutes)
        /// </summary>
        [Required]
        public virtual int RefreshTokenLifeTime { get; set; }

        /// <summary>
        /// Allowed origin
        /// </summary>
        [Required]
        [StringLength(255)]
        public virtual string AllowedOrigin { get; set; }

        /// <summary>
        /// Current audience has administrative access
        /// </summary>
        [Required]
        public virtual bool HasAdministrativeAccess { get; set; }
    }
}
