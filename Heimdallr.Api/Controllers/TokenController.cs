using System.IdentityModel.Tokens.Jwt;
using ZenProgramming.Heimdallr.Api.Controllers.Common;
using ZenProgramming.Heimdallr.Api.Helpers;
using ZenProgramming.Heimdallr.Api.Models.Requests;
using ZenProgramming.Heimdallr.Api.Models.Responses;
using ZenProgramming.Heimdallr.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ZenProgramming.Heimdallr.Api.Controllers
{
    /// <summary>
    /// Controller for manage tokens
    /// </summary>
    [Route("oauth2")]
    public class TokenController : ApiControllerBase
    {
        /// <summary>
        /// Generate OAuth2 token response for provided credentials
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Returns response</returns>
        [HttpPost]
        [Route("token")]
        [ProducesResponseType(typeof(TokenResponse), 200)]
        public IActionResult Token(TokenRequest request)
        {
            //Validazione argomenti
            if (request == null)
                return BadRequest();

            //Valido la richiesta sulla base delle annotazioni
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //*** Usage : http://[application-base-url]/oauth2/token
            //*** Body (caso "password"): 
            //*** {
            //***     "userName": "Administrator", 
            //***     "password": "password", 
            //***     "grantType": "password"
            //***     "clientId": "heimdallr.authentication.api"
            //*** }
            //*** Body (caso "refresh_token"): 
            //*** {
            //***     "refreshToken": "345678900045355678903456789", 
            //***     "grantType": "refresh_token"
            //***     "clientId": "miazzo.authentication.api"
            //*** }
            //*** Encoding : "x-www-form-urlencoded"

            //Username individuato
            string existingUserName;

            //Se siamo in modalità "password", eseguo la validazione 
            //sulla base di username e password passate nella request
            if (request.GrantType == "password")
            {
                //Se non ho username, bad request
                if (string.IsNullOrEmpty(request.UserName))
                {
                    //Aggiungo la validazione nel modello
                    ModelState.AddModelError("UserName", "User name is required");
                    return BadRequest(ModelState);
                }

                //Se non ho password, bad request
                if (string.IsNullOrEmpty(request.Password))
                {
                    //Aggiungo la validazione nel modello
                    ModelState.AddModelError("Password", "Password is required");
                    return BadRequest(ModelState);
                }

                //Tento il sign-in con il service layer
                var result = Layer.SignIn(request.UserName, request.Password);

                //Se non ho risultato, unauthorized
                if (result == null)
                    return Unauthorized();

                //Salvo il nome utente individuato
                existingUserName = result.UserName;
            }

            //Altrimenti, se utilizzo il RefreshToken
            else if (request.GrantType == "refresh_token")
            {
                //Se non ho il refresh token, bad request
                if (string.IsNullOrEmpty(request.RefreshToken))
                {
                    //Aggiungo la validazione nel modello
                    ModelState.AddModelError("RefreshToken", "Refresh token is required");
                    return BadRequest(ModelState);
                }

                //Tento il recupero del modello "RefreshToken"
                RefreshToken refreshToken = Layer.GetRefreshToken(request.RefreshToken);
                if (refreshToken == null)
                    return Unauthorized();

                //Salvo l'utente associato
                existingUserName = refreshToken.UserName;
            }

            //Altrimenti, se non ho specificato uno dei casi precedenti
            else
            {
                //Caso in cui non viene passato nessun grant valido
                ModelState.AddModelError("", $"Provided grant type '{request.GrantType}' is invalid");
                return BadRequest(ModelState);
            }

            //Recupero l'utente
            User user = Layer.GetUserByUserName(existingUserName);
            if (user == null)
                return NotFound();

            //Recupero l'audience sulla base del client id
            Audience audience = Layer.GetAudienceByClientId(request.ClientId);
            if (audience == null)
                return NotFound();

            //Creo il nuovo refresh token sul database utilizzando il nome
            //utente e l'audience già recuperata al passo precedente
            RefreshToken generatedOrUpdatedRefreshToken = Layer.CreateOrUpdateRefreshToken(user, audience);

            //Creo il token
            var token = JwtUtils.GenerateJwtSecurityToken(user, audience);

            //Creo il JWT
            var jwt = new JwtSecurityTokenHandler();
            string value = jwt.WriteToken(token);

            //Compongo la response
            var response = new TokenResponse
            {
                AccessToken = value,
                RefreshToken = generatedOrUpdatedRefreshToken.TokenHash,
                Expiration = token.ValidTo
            };

            //Ritorno conferma
            return Ok(response);
        }              
    }
}
