using System.Collections.Generic;
using Chakra.Core.Configurations;

namespace ZenProgramming.Heimdallr.Configurations
{
    /// <summary>
    /// Application configuration
    /// </summary>
    public class HeimdallrConfiguration: IApplicationConfigurationRoot
    {
        /// <summary>
        /// Name of current environment
        /// </summary>
        public string EnvironmentName { get; set; }

        /// <summary>
        /// Platform settings
        /// </summary>
        public PlatformConfiguration Platform { get; set; }

        /// <summary>
        /// Storage settings
        /// </summary>
        public StorageConfiguration Storage { get; set; }

        /// <summary>
        /// List of connection strings
        /// </summary>
        public IList<ConnectionStringConfiguration> ConnectionStrings { get; set; }        
    }    
}
