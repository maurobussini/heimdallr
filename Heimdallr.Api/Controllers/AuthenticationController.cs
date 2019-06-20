using ZenProgramming.Heimdallr.Api.Controllers.Common;
using ZenProgramming.Heimdallr.Api.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ZenProgramming.Heimdallr.Api.Controllers
{
    /// <summary>
    /// Controller for authentication
    /// </summary>
    [Route("api/Authentication")]
    public class AuthenticationController : ApiControllerBase
    {
        /// <summary>
        /// Executes sign-in with credentials
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Returns action result</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("SignIn")]
        public IActionResult SignIn([FromBody]SignInRequest request)
        {
            //Validazione argomenti
            if (request == null)
                return BadRequest();

            //Validazione del modello
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Tento il sign-in con il service layer
            var result = Layer.SignIn(request.UserName, request.Password);

            //Se non ho ricevuto il risultato
            if (result == null)
                return Unauthorized();

            //Ritorno il risultato
            return Ok(result);
        }
    }
}