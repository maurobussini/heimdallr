using System.ComponentModel.DataAnnotations;

namespace ZenProgramming.Heimdallr.Api.Models.Requests
{
    /// <summary>
    /// Request for single audience
    /// </summary>
    public class AudienceRequest
    {
        /// <summary>
        /// Client id for single audience
        /// </summary>
        [Required]
        [StringLength(255)]
        public string ClientId { get; set; }
    }
}
