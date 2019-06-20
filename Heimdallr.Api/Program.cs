using System;
using System.Collections.Generic;
using Chakra.Core.Configurations;
using ZenProgramming.Heimdallr.Configurations;
using ZenProgramming.Heimdallr.EntityFramework.Contexts;
using ZenProgramming.Heimdallr.Mocks.Scenarios;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using ZenProgramming.Chakra.Core.Configurations.Utils;
using ZenProgramming.Chakra.Core.Data;
using ZenProgramming.Chakra.Core.Data.Mockups;
using ZenProgramming.Chakra.Core.Data.Mockups.Scenarios;
using ZenProgramming.Chakra.Core.EntityFramework.Data;

namespace ZenProgramming.Heimdallr.Api
{
    /// <summary>
    /// Main application class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args">Arguments</param>
        public static void Main(string[] args)
        {
            //Select provider for data storage
            SettingsUtils.Switch(ConfigurationFactory<HeimdallrConfiguration>.Instance.Storage.Scenario, new Dictionary<string, Action>
            {
                { "Basic", () => ScenarioFactory.Initialize(new BasicScenario()) },
                { "Demo", () => ScenarioFactory.Initialize(new DemoScenario()) }
            });

            //Select provider for data storage
            SettingsUtils.Switch(ConfigurationFactory<HeimdallrConfiguration>.Instance.Storage.Provider, new Dictionary<string, Action>
            {
                { "Mockup", SessionFactory.RegisterDefaultDataSession<MockupDataSession> },
                { "EntityFramework", SessionFactory.RegisterDefaultDataSession<EntityFrameworkDataSession<HeimdallrDbContext>> }
            });           

            ////Applico l'inizializer al contex corrente
            //IDataSession session = SessionFactory.OpenSession();
            //session.As<EntityFrameworkDataSession<HeimdallrDbContext>>().Context
            //    .ApplyInitializer<HeimdallrDbContext, DefaultDbContextInitializer>();

            //Avvio l'hosting
            BuildWebHost(args).Run();

            //https://stackoverflow.com/questions/45715394/asp-net-core-2-0-bearer-auth-without-identity
        }

        /// <summary>
        /// Host definition
        /// </summary>
        /// <param name="args">Arguments</param>
        /// <returns>Returns web host</returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
