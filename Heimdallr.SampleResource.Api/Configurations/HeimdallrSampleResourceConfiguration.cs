using System.Collections.Generic;
using Chakra.Core.Configurations;

namespace ZenProgramming.Heimdallr.Configurations
{
    /// <summary>
    /// Application configuration
    /// </summary>
    public class HeimdallrSampleResourceConfiguration: IApplicationConfigurationRoot
    {
        /// <summary>
        /// Name of current environment
        /// </summary>
        public string EnvironmentName { get; set; }
    }    
}
