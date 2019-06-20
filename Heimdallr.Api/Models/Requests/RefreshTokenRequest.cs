namespace ZenProgramming.Heimdallr.Api.Models.Requests
{
    /// <summary>
    /// Request for refresh token
    /// </summary>
    public class RefreshTokenRequest
    {
        /// <summary>
        /// Token hash
        /// </summary>
        public string TokenHash { get; set; }
    }
}
