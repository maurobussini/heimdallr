using System.Linq;
using ZenProgramming.Heimdallr.Data.Repositories;
using ZenProgramming.Heimdallr.Entities;
using ZenProgramming.Heimdallr.Mocks.Scenarios.Common;
using ZenProgramming.Chakra.Core.Data;
using ZenProgramming.Chakra.Core.Data.Repositories.Attributes;
using ZenProgramming.Chakra.Core.Data.Repositories.Mockups;

namespace ZenProgramming.Heimdallr.Mocks.Data.Repositories
{
    /// <summary>
    /// Mock implementation for repository of "RefreshToken"
    /// </summary>
    [Repository]
    public class MockRefreshTokenRepository:  MockupRepositoryBase<RefreshToken, IHeimdallrScenario>, IRefreshTokenRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataSession"></param>
        public MockRefreshTokenRepository(IDataSession dataSession) 
            : base(dataSession, scn => scn.RefreshTokens) { }

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
            return MockedEntities.SingleOrDefault(s =>
                s.ClientId == clientId &&
                s.UserName.ToLower() == userName.ToLower());
        }
    }
}
