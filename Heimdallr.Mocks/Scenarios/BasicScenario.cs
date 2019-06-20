using System;
using ZenProgramming.Heimdallr.Entities;
using ZenProgramming.Heimdallr.Mocks.Scenarios.Common;
using ZenProgramming.Chakra.Core.Utilities.Security;

namespace ZenProgramming.Heimdallr.Mocks.Scenarios
{
    /// <summary>
    /// Basic scenario with platform administrator
    /// </summary>
    public class BasicScenario: HeimdallrScenarioBase
    {
        /// <summary>
        /// Initialize entities for scenario
        /// </summary>
        public override void InitializeEntities()
        {
            //Aggiungo l'amministrator
            Users.Add(new User
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
                PhotoBinary = new byte[] {}
            });

            //Creo l'audience corrente (Nativa)
            Audiences.Add(new Audience
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
        }
    }
}
