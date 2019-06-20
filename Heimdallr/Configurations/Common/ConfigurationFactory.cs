﻿//using System;
//using System.Diagnostics;
//using System.IO;
//using Microsoft.Extensions.Configuration;

//namespace ZenProgramming.Heimdallr.Configurations.Common
//{
//    /// <summary>
//    /// Factory for read configuration of current application
//    /// </summary>
//    /// <typeparam name="TApplicationConfiguration">Type of configuration typized structure</typeparam>
//    public static class ConfigurationFactory<TApplicationConfiguration>
//        where TApplicationConfiguration: class, IApplicationConfigurationRoot, new()
//    {
//        /// <summary>
//        /// Lazy initializer for application configuration
//        /// </summary>
//        private static readonly Lazy<TApplicationConfiguration> _Instance = new Lazy<TApplicationConfiguration>(Initialize);

//        /// <summary>
//        /// Singleton instance for configuration
//        /// </summary>
//        public static TApplicationConfiguration Instance => _Instance.Value;

//        /// <summary>
//        /// Initializes application configuration
//        /// </summary>
//        /// <returns>Returns instance</returns>
//        private static TApplicationConfiguration Initialize()
//        {
//            //Recupero le variabili di ambiente possibili
//            var aspNetCore = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
//            var dotNetCore = Environment.GetEnvironmentVariable("DOTNETCORE_ENVIRONMENT");

//            //Se ho quella asp.net utilizzo quella
//            string environmentName = !string.IsNullOrEmpty(aspNetCore)
//                ? aspNetCore
//                : dotNetCore;

//            //Default settings file
//            const string DefaultAppSettings = "appsettings.json";
//            const string TemplatedAppSettings = "appsettings.{0}.json";

//            //Nome del file di settings
//            string settingsFileName = string.IsNullOrEmpty(environmentName)
//                ? DefaultAppSettings
//                : string.Format(TemplatedAppSettings, environmentName).ToLower();

//            //Composizione del percorso del file
//            string path = Path.Combine(Directory.GetCurrentDirectory(), settingsFileName);

//            //Se il file non esiste, imposto il default
//            if (!File.Exists(path))
//                settingsFileName = DefaultAppSettings;

//            //Creo il builder della configurazione
//            Debug.WriteLine($"Using setting file '{settingsFileName}' with environment '{environmentName}'...");
//            var builder = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddJsonFile(settingsFileName, optional: false, reloadOnChange: true);

//            //Buildo la configurazione
//            IConfigurationRoot configuration = builder.Build();

//            //Creo una nuova instanza della classe strongly-types
//            TApplicationConfiguration instance = new TApplicationConfiguration();

//            //Eseguo il binding delle informazioni sulla classe statica
//            configuration.Bind(instance);

//            //Imposto il nome dell'environment usato
//            instance.EnvironmentName = environmentName;

//            //Ritorno l'istanza
//            return instance;
//        }
//    }    
//}
