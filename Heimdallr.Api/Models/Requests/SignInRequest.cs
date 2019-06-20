using System.ComponentModel.DataAnnotations;

namespace ZenProgramming.Heimdallr.Api.Models.Requests
{
    /// <summary>
    /// Request for sign-in
    /// </summary>
    public class SignInRequest
    {
        /// <summary>
        /// User name
        /// </summary>
        [Required]
        [StringLength(255)]
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Password { get; set; }
    }
}
