using System;

namespace ZenProgramming.Heimdallr.Api.Models
{
    /// <summary>
    /// Contract for user
    /// </summary>
    public class UserContract
    {        
        /// <summary>
        /// Username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Person name
        /// </summary>
        public string PersonName { get; set; }

        /// <summary>
        /// Person surname
        /// </summary>
        public string PersonSurname { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Is enabled
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Last access date
        /// </summary>
        public DateTime? LastAccessDate { get; set; }

        /// <summary>
        /// Is locked
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// Name of provided used for sign-in
        /// </summary>
        public string SignInProvider { get; set; }

        #region Status fields

        /// <summary>
        /// User must change password
        /// </summary>
        public bool? MustChangePassword { get; set; }

        /// <summary>
        /// Permission to change password on platform
        /// </summary>
        public bool? CanChangePassword { get; set; }

        /// <summary>
        /// Permission to change email on platform
        /// </summary>
        public bool? CanChangeEmail { get; set; }
        #endregion
    }
}
