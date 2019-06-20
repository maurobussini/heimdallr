using System;
using System.Linq;
using ZenProgramming.Heimdallr.Entities;
using ZenProgramming.Heimdallr.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using ZenProgramming.Chakra.Core.Utilities.Security;

namespace ZenProgramming.Heimdallr.EntityFramework.Initializers
{
    /// <summary>
    /// Represents default database initializer
    /// </summary>
    public class DefaultDbContextInitializer: IDbContextInitializer<HeimdallrDbContext>
    {
        /// <summary>
        /// Seed on provided DbContext initialization data
        /// </summary>
        /// <param name="instance">DbContext instance</param>
        public void Seed(HeimdallrDbContext instance)
        {
            //Arguments validation
            if (instance == null) throw new ArgumentNullException(nameof(instance));

            //If has NO users, create
            if (!instance.Users.Any(u => u.UserName == "Administrator"))
            {
                //Aggiungo l'amministrator
                instance.Users.Add(new User
                {
                    Id = Guid.NewGuid().ToString("D"),
                    UserName = "Administrator",
                    Email = "digital@ZenProgramming.it",
                    IsEnabled = true,
                    IsLocked = false,
                    PersonName = "Digital",
                    PersonSurname = "ZenProgramming",
                    PasswordHash = ShaProcessor.Sha256Encrypt("password"),
                    LastAccessDate = DateTime.UtcNow.AddDays(-1),
                    PhotoBinary = new byte[] { }
                });

                //Save changes
                instance.SaveChanges();
            }

            //If has NO audience, create
            if (!instance.Audiences.Any(a => a.ClientId == "heimdallr.api"))
            {
                //Creo l'audience corrente (Nativa)
                instance.Audiences.Add(new Audience
                {
                    Id = Guid.NewGuid().ToString("D"),
                    Name = "Heimdallr Api",
                    ClientId = "heimdallr.api",
                    ClientSecret = "IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw",
                    HasAdministrativeAccess = true,
                    IsEnabled = true,
                    IsNative = true,
                    AllowedOrigin = "*",
                    RefreshTokenLifeTime = 30
                });

                //Save changes
                instance.SaveChanges();
            }
        }
    }

    /// <summary>
    /// Interface for database initializer
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public interface IDbContextInitializer<in TDbContext>
        where TDbContext: DbContext
    {
        /// <summary>
        /// Seed on provided DbContext initialization data
        /// </summary>
        /// <param name="instance">DbContext instance</param>
        void Seed(TDbContext instance);
    }
}
