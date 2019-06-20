using System;
using System.ComponentModel.DataAnnotations;
using ZenProgramming.Chakra.Core.DataAnnotations;
using ZenProgramming.Chakra.Core.DataAnnotations.Enums;
using ZenProgramming.Chakra.Core.Entities;
using ZenProgramming.Chakra.OAuth.Entities;

namespace ZenProgramming.Heimdallr.Entities
{
    /// <summary>
    /// Entity for user
    /// </summary>
    public class User: ModernEntityBase, IUser
    {
        /// <summary>
        /// Username
        /// </summary>
        [Required]
        [StringLength(255)]
        [RegularExpression(@"^[a-zA-Z0-9_@\-\.]+$")]
        public virtual string UserName { get; set; }

        /// <summary>
        /// Password hash (SHA-384)
        /// </summary>
        [StringLength(1024)]
        public virtual string PasswordHash { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [StringLength(255)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.'\+&=]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$")]
        public virtual string Email { get; set; }

        /// <summary>
        /// Person name
        /// </summary>
        [StringLength(255)]
        public virtual string PersonName { get; set; }

        /// <summary>
        /// Person surname
        /// </summary>
        [StringLength(255)]
        public virtual string PersonSurname { get; set; }

        /// <summary>
        /// Flag for enable user
        /// </summary>
        [Required]
        public virtual bool IsEnabled { get; set; }

        /// <summary>
        /// Last access date
        /// </summary>
        [TimeLimit(RangeLimit.Min, 1900, 1, 1)]
        [TimeLimit(RangeLimit.Max, 2100, 12, 31)]
        public virtual DateTime? LastAccessDate { get; set; }

        /// <summary>
        /// Flag for locked user (ex. too much tentatives)
        /// </summary>
        [Required]
        public virtual bool IsLocked { get; set; }        

        /// <summary>
        /// Binary data for photo
        /// </summary>
        public virtual byte[] PhotoBinary { get; set; }

        //HINT: Here custom fields
    }
}
