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
    /// Repository on EntityFramework engine for "User"
    /// </summary>
    [Repository]
    public class EfUserRepository : EntityFrameworkRepositoryBase<User, HeimdallrDbContext>, IUserRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataSession">Data session</param>
        public EfUserRepository(IDataSession dataSession) 
            : base(dataSession, dbc => dbc.Users) { }

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
            return Collection.SingleOrDefault(s => s.UserName.ToLower() == userName.ToLower());
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
            return Collection.SingleOrDefault(s => s.Email.ToLower() == email.ToLower());
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
            return Collection.Count(s => s.Email.ToLower() == email.ToLower());
        }
    }
}
