namespace ZenProgramming.Heimdallr.Api.Models
{
    /// <summary>
    /// Contract for audience
    /// </summary>
    public class AudienceContract
    {
        /// <summary>
        /// Audience identifier
        /// </summary>
        public string AudienceId { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Client id
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Client secret
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Is native application
        /// </summary>
        public bool IsNative { get; set; }

        /// <summary>
        /// Is enabled
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Refresh token life time (in minutes)
        /// </summary>
        public int RefreshTokenLifeTime { get; set; }

        /// <summary>
        /// Allowed origin
        /// </summary>
        public string AllowedOrigin { get; set; }

        /// <summary>
        /// Current audience has administrative access
        /// </summary>
        public bool HasAdministrativeAccess { get; set; }
    }
}
