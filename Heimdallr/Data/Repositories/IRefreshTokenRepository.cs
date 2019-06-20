using ZenProgramming.Heimdallr.Entities;
using ZenProgramming.Chakra.Core.Data.Repositories;

namespace ZenProgramming.Heimdallr.Data.Repositories
{
    /// <summary>
    /// Interface for repository of "RefreshToken"
    /// </summary>
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        /// <summary>
        /// Get single refresh token using clientId and user name
        /// </summary>
        /// <param name="clientId">ClientId</param>
        /// <param name="userName">Username</param>
        /// <returns>Returns user or null</returns>
        RefreshToken GetSingle(string clientId, string userName);
    }
}
