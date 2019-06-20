using System;
using System.Collections.Generic;
using System.Reflection;
using Chakra.Core.Configurations;
using ZenProgramming.Heimdallr.Configurations;
using ZenProgramming.Heimdallr.ServiceLayers;
using ZenProgramming.Heimdallr.Utils;
using ZenProgramming.Chakra.Core.Data;
using ZenProgramming.Chakra.Core.Extensions;
using ZenProgramming.Chakra.Core.Utilities.Server;
using ZenProgramming.Chakra.Core.Utilities.Server.ConsoleMenu;

namespace ZenProgramming.Heimdallr.Terminal.Procedures
{
    public class SetupProcedures: ConsoleMenuContainerBase<SetupProcedures>
    {
        /// <summary>
        /// Generates menu entries
        /// </summary>
        /// <returns></returns>
        protected override IList<ConsoleMenuElement> GenerateElements()
        {
            return new List<ConsoleMenuElement>
            {
                new ConsoleMenuElement("connection", "Check database connection", CheckDatabaseConnection),
                new ConsoleMenuElement("generate", "Generate client secret", GenerateClientSecret),
                new ConsoleMenuElement("info", "Get application information", GetApplicationInfo)
            };
        }

        private void GetApplicationInfo()
        {
            //Compose name and version number
            string name = $"{Assembly.GetExecutingAssembly().GetName().Name} " +
                          $"v{Assembly.GetExecutingAssembly().GetName().Version.Major}" +
                          $".{Assembly.GetExecutingAssembly().GetName().Version.Minor}" +
                          $".{Assembly.GetExecutingAssembly().GetName().Version.Build}";

            var env = ConfigurationFactory<HeimdallrConfiguration>.Instance.EnvironmentName;
            Console.WriteLine($"Hello World! {name}: env {env}");
        }

        private void GenerateClientSecret()
        {
            //Generate random ClientSecret
            var clientSecret = TokenUtils.GenerateRandomClientSecret();
            ConsoleUtils.WriteColorLine(ConsoleColor.Yellow, $"Generated random client secret: {clientSecret}");
        }

        private void CheckDatabaseConnection()
        {
            //Open new data session
            ConsoleUtils.WriteColorLine(ConsoleColor.Yellow, "Opening connection...");
            using (IDataSession dataSession = SessionFactory.OpenSession())
            {
                //Open service layer
                ConsoleUtils.WriteColorLine(ConsoleColor.Yellow, "Creating service layer...");
                using (IdentityServiceLayer layer = new IdentityServiceLayer(dataSession))
                {
                    //Fetch list of users
                    ConsoleUtils.WriteColorLine(ConsoleColor.Yellow, "Fetching users...");
                    var users = layer.FetchAllUsers();

                    //Print list of users
                    users.Each(u =>  ConsoleUtils.WriteColorLine(ConsoleColor.Green, $"Found {u.UserName} => {u.Email}") );
                }
            }

            //Exiting
            ConsoleUtils.WriteColorLine(ConsoleColor.Cyan, "Program completed!");
            Console.ReadLine();
        }
    }
}
