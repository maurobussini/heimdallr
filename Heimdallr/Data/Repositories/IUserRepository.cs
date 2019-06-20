using ZenProgramming.Heimdallr.Entities;
using ZenProgramming.Chakra.Core.Data.Repositories;

namespace ZenProgramming.Heimdallr.Data.Repositories
{
    /// <summary>
    /// Interface for repository of "User"
    /// </summary>
    public interface IUserRepository: IRepository<User>
    {
        /// <summary>
        /// Get user (case-insensitive) by username
        /// </summary>
        /// <param name="userName">User name</param>
        /// <returns>Returns user or null</returns>
        User GetUserByUserName(string userName);

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Returns user or null</returns>
        User GetByEmail(string email);

        /// <summary>
        /// Count users with matching email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Returns count</returns>
        int CountByEmail(string email);
    }
}
