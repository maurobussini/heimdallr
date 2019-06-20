using System;
using ZenProgramming.Heimdallr.Entities;
using ZenProgramming.Chakra.Core.Utilities.Security;

namespace ZenProgramming.Heimdallr.Mocks.Scenarios
{
    /// <summary>
    /// Demo scenario
    /// </summary>
    public class DemoScenario: BasicScenario
    {
        /// <summary>
        /// Initialize entities for scenario
        /// </summary>
        public override void InitializeEntities()
        {
            //Inizializzazione base
            base.InitializeEntities();

            //Aggiungo l'amministrator
            Users.Add(new User
            {
                Id = Guid.NewGuid().ToString("D"),
                UserName = "mario.rossi",
                Email = "mario.rossi@ZenProgramming.it",
                IsEnabled = true,
                IsLocked = false,
                PersonName = "Mario",
                PersonSurname = "Rossi",
                PasswordHash = ShaProcessor.Sha256Encrypt("password"),
                LastAccessDate = DateTime.UtcNow.AddDays(-1),
                PhotoBinary = new byte[] { }
            });

            //Creo l'audience corrente (Nativa)
            Audiences.Add(new Audience
            {
                Id = Guid.NewGuid().ToString("D"),
                Name = "Sample Resource",
                ClientId = "sample.resource",
                ClientSecret = "_IqrVEUhwO19o9nQ-9DtOfzYAL5THZrGcWgCO1-Xa0k",
                HasAdministrativeAccess = false,
                IsEnabled = true,
                IsNative = true,
                AllowedOrigin = "*",
                RefreshTokenLifeTime = 30
            });
        }
    }
}
