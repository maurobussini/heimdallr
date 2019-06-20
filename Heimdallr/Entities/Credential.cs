using System.ComponentModel.DataAnnotations;
using ZenProgramming.Chakra.Core.Entities;

namespace ZenProgramming.Heimdallr.Entities
{
    /// <summary>
    /// Model for handle credentials on the platform
    /// </summary>
    public class Credential: ModernEntityBase
    {
        /// <summary>
        /// Reference user name
        /// </summary>
        [Required]
        [StringLength(255)]
        public string UserName { get; set; }

        /// <summary>
        /// Name of provider used for authenticate
        /// </summary>
        [Required]
        [StringLength(255)]
        public string ProviderName { get; set; }

        /// <summary>
        /// Password hash (SHA-384)
        /// </summary>
        [StringLength(1024)]
        public virtual string PasswordHash { get; set; }
    }
}
