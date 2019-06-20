namespace ZenProgramming.Heimdallr.Configurations
{
    /// <summary>
    /// Configuration options for platform
    /// </summary>
    public class PlatformConfiguration
    {
        /// <summary>
        /// Issuer
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Audience client id
        /// </summary>
        public string AudienceClientId { get; set; }

        /// <summary>
        /// Audience client secret
        /// </summary>
        public string AudienceClientSecret { get; set; }
    }
}
