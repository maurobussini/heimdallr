using System;
using System.ComponentModel.DataAnnotations;
using ZenProgramming.Chakra.Core.DataAnnotations;
using ZenProgramming.Chakra.Core.DataAnnotations.Enums;
using ZenProgramming.Chakra.Core.Entities;

namespace ZenProgramming.Heimdallr.Entities
{
    /// <summary>
    /// Entity for refresh token
    /// </summary>
    public class RefreshToken: ModernEntityBase
    {
        /// <summary>
        /// Hash for refresh token
        /// </summary>
        [Required]
        [StringLength(255)]
        public virtual string TokenHash { get; set; }

        /// <summary>
        /// ClientdId
        /// </summary>
        [Required]
        [StringLength(255)]
        public virtual string ClientId { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        [Required]
        [StringLength(255)]
        public virtual string UserName { get; set; }

        /// <summary>
        /// Issue time in UTC format
        /// </summary>
        [Required]
        [TimeLimit(RangeLimit.Min, 1900, 1, 1)]
        [TimeLimit(RangeLimit.Max, 2100, 12, 31)]
        public virtual DateTime IssuedUtc { get; set; }

        /// <summary>
        /// Expire time in UTC format
        /// </summary>
        [Required]
        [TimeLimit(RangeLimit.Min, 1900, 1, 1)]
        [TimeLimit(RangeLimit.Max, 2100, 12, 31)]
        public virtual DateTime ExpiresUtc { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        [Required]
        [StringLength(2000)]
        public virtual string ProtectedTicket { get; set; }
    }
}
