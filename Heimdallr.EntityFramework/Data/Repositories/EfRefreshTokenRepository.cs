using System.Linq;
using ZenProgramming.Heimdallr.Data.Repositories;
using ZenProgramming.Heimdallr.Entities;
using ZenProgramming.Heimdallr.EntityFramework.Contexts;
using ZenProgramming.Chakra.Core.Data;
using ZenProgramming.Chakra.Core.Data.Repositories.Attributes;
using ZenProgramming.Chakra.Core.EntityFramework.Data.Repositories;

namespace ZenProgramming.Heimdallr.EntityFramework.Data.Repositories
{
    /// <summary>
    /// Repository on EntityFramework engine for "RefreshToken"
    /// </summary>
    [Repository]
    public class EfRefreshTokenRepository : EntityFrameworkRepositoryBase<RefreshToken, HeimdallrDbContext>, IRefreshTokenRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataSession">Data session</param>
        public EfRefreshTokenRepository(IDataSession dataSession) 
            : base(dataSession, dbc => dbc.RefreshTokens) { }

        /// <summary>
        /// Get single refresh token using clientId and user name
        /// </summary>
        /// <param name="clientId">ClientId</param>
        /// <param name="userName">Username</param>
        /// <returns>Returns user or null</returns>
        public RefreshToken GetSingle(string clientId, string userName)
        {
            //Validazione argomenti
            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(userName))
                return null;

            //Utilizzo il metodo base (accetto una certa "imperfezione" dello user name)
            return Collection.SingleOrDefault(s =>
                s.ClientId == clientId &&
                s.UserName.ToLower() == userName.ToLower());
        }
    }
}
