using System.Reflection;
using Chakra.Core.Configurations;
using ZenProgramming.Heimdallr.Api.Controllers.Common;
using ZenProgramming.Heimdallr.Configurations;
using Microsoft.AspNetCore.Mvc;

namespace ZenProgramming.Heimdallr.Api.Controllers
{
    /// <summary>
    /// Controller for application diagnostics
    /// </summary>
    [Route("api/Diagnostics")]
    public class DiagnosticsController: ApiControllerBase
    {
        /// <summary>
        /// Get application echo page
        /// </summary>
        /// <returns>Returns action result</returns>
        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            //Recupero le informazioni sulla versione applicativa
            var version = string.Format("v{0}.{1}.{2}",
                Assembly.GetExecutingAssembly().GetName().Version.Major,
                Assembly.GetExecutingAssembly().GetName().Version.Minor,
                Assembly.GetExecutingAssembly().GetName().Version.Build);

            //Nome dell'assembly corrente
            var name = Assembly.GetExecutingAssembly().GetName().Name;

            //Environment impostato
            var env = ConfigurationFactory<HeimdallrConfiguration>.Instance.EnvironmentName;

            //Compongo la stringa di output
            string output = $"{name} {version} is running on environment {env}...";

            //Ritorno il contenuto
            return Content(output);
        }
    }
}
