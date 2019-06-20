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
    /// Mock implementation for repository of "User"
    /// </summary>
    [Repository]
    public class MockUserRepository:  MockupRepositoryBase<User, IHeimdallrScenario>, IUserRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataSession"></param>
        public MockUserRepository(IDataSession dataSession) 
            : base(dataSession, scn => scn.Users) { }

        /// <summary>
        /// Get user (case-insensitive) by username
        /// </summary>
        /// <param name="userName">User name</param>
        /// <returns>Returns user or null</returns>
        public User GetUserByUserName(string userName)
        {
            //Validazione argomenti
            if (string.IsNullOrEmpty(userName))
                return null;

            //Utilizzo il metodo base
            return MockedEntities.SingleOrDefault(s => s.UserName.ToLower() == userName.ToLower());
        }

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Returns user or null</returns>
        public User GetByEmail(string email)
        {
            //Validazione argomenti
            if (string.IsNullOrEmpty(email))
                return null;

            //Utilizzo il metodo base
            return MockedEntities.SingleOrDefault(s => s.Email.ToLower() == email.ToLower());
        }

        /// <summary>
        /// Count users with matching email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Returns count</returns>
        public int CountByEmail(string email)
        {
            //Validazione argomenti
            if (string.IsNullOrEmpty(email))
                return 0;

            //Eseguo il conteggio delle entità
            return MockedEntities.Count(s => s.Email.ToLower() == email.ToLower());
        }
    }
}
