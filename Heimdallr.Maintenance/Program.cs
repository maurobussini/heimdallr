using System;
using System.Collections.Generic;
using Chakra.Core.Configurations;
using ZenProgramming.Heimdallr.Configurations;
using ZenProgramming.Heimdallr.EntityFramework.Contexts;
using ZenProgramming.Heimdallr.Mocks.Scenarios;
using ZenProgramming.Heimdallr.Terminal.Procedures;
using ZenProgramming.Chakra.Core.Configurations.Utils;
using ZenProgramming.Chakra.Core.Data;
using ZenProgramming.Chakra.Core.Data.Mockups;
using ZenProgramming.Chakra.Core.Data.Mockups.Scenarios;
using ZenProgramming.Chakra.Core.EntityFramework.Data;
using ZenProgramming.Chakra.Core.Utilities.Server;
using ZenProgramming.Chakra.Core.Utilities.Server.ConsoleMenu;

namespace ZenProgramming.Heimdallr.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            //Select provider for data storage
            SettingsUtils.Switch(ConfigurationFactory<HeimdallrConfiguration>.Instance.Storage.Scenario, new Dictionary<string, Action>
            {
                { "Basic", () => ScenarioFactory.Initialize(new BasicScenario()) }
            });

            //Select provider for data storage
            SettingsUtils.Switch(ConfigurationFactory<HeimdallrConfiguration>.Instance.Storage.Provider, new Dictionary<string, Action>
            {
                { "Mockup", SessionFactory.RegisterDefaultDataSession<MockupDataSession> },
                { "EntityFramework", SessionFactory.RegisterDefaultDataSession<EntityFrameworkDataSession<HeimdallrDbContext>> }
            });

            //Main menu
            ConsoleUtils.RenderMenu("Heimdallr Maintenance", new List<ConsoleMenuElement>
            {
                new ConsoleMenuElement("s", "Setup procedures", SetupProcedures.Current.Summary)
            });
        }
    }
}
