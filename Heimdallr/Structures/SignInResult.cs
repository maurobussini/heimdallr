using System;

namespace ZenProgramming.Heimdallr.Structures
{
    /// <summary>
    /// Results for sign-in
    /// </summary>
    public class SignInResult
    {
        /// <summary>
        /// Username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Person name
        /// </summary>
        public string PersonName { get; set; }

        /// <summary>
        /// Person surname
        /// </summary>
        public string PersonSurname { get; set; }

        /// <summary>
        /// Flag for enable user
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Last access date
        /// </summary>
        public DateTime? LastAccessDate { get; set; }

        /// <summary>
        /// Flag for locked user (ex. too much tentatives)
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// Provider used for sign-in
        /// </summary>
        public string SignInProvider { get; set; }
    }
}
