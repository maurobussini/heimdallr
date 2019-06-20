using System.Collections.Generic;
using ZenProgramming.Heimdallr.Api.Controllers.Common;
using ZenProgramming.Heimdallr.Api.Helpers;
using ZenProgramming.Heimdallr.Api.Models;
using ZenProgramming.Heimdallr.Api.Models.Requests;
using ZenProgramming.Heimdallr.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZenProgramming.Chakra.Core.Extensions;

namespace ZenProgramming.Heimdallr.Api.Controllers
{
    /// <summary>
    /// Controller for manage audiences and tokens
    /// </summary>
    [Authorize]
    [Route("api/Audiences")]
    public class AudiencesController : ApiControllerBase
    {
        /// <summary>
        /// Create new audience on the platform
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Returns action result</returns>
        [HttpPost]
        [Route("CreateAudience")]
        public IActionResult CreateAudience([FromBody]CreateAudienceRequest request)
        {
            //Validazione argomenti
            if (request == null)
                return BadRequest();

            //Se non è valida
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Recupero l'utente per username
            User user = Layer.GetUserByUserName(User.Identity.Name);
            if (user == null)
                return NotFound();

            //Se l'utente non ha diritti amministrativi, esco
            if (!Layer.HasAdministrativeGrants(user))
                return Unauthorized();

            //Eseguo la creazione dell'audience
            Audience entity = new Audience
            {
                Name = request.Name,
                IsEnabled = request.IsEnabled,
                IsNative = request.IsNative,
                RefreshTokenLifeTime = request.RefreshTokenLifeTime,
                AllowedOrigin = request.AllowedOrigin
            };

            //Eseguo il salvataggio dell'elemento
            var validations = Layer.SaveAudience(entity);

            //Se ho validazioni fallite
            if (validations.Count > 0)
                return BadRequest(validations);

            //Se è tutto ok, genero il contratto
            return Ok(ContractUtils.GenerateContract(entity));
        }

        /// <summary>
        /// Updates existing audience on the platform
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Returns action result</returns>
        [HttpPost]
        [Route("UpdateAudience")]
        public IActionResult UpdateAudience([FromBody]UpdateAudienceRequest request)
        {
            //Validazione argomenti
            if (request == null)
                return BadRequest();

            //Se non è valida
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Recupero l'utente per username
            User user = Layer.GetUserByUserName(User.Identity.Name);
            if (user == null)
                return NotFound();

            //Se l'utente non ha diritti amministrativi, esco
            if (!Layer.HasAdministrativeGrants(user))
                return Unauthorized();

            //Tento il recupero dell'audience
            Audience entity = Layer.GetAudienceByClientId(request.ClientId);
            if (entity == null)
                return NotFound();

            //Aggiorno le informazioni dell'entità
            entity.Name = request.Name;
            entity.IsEnabled = request.IsEnabled;
            entity.IsNative = request.IsNative;
            entity.RefreshTokenLifeTime = request.RefreshTokenLifeTime;
            entity.AllowedOrigin = request.AllowedOrigin;
            entity.HasAdministrativeAccess = request.HasAdministrativeAccess;

            //Eseguo il salvataggio dell'elemento
            var validations = Layer.SaveAudience(entity);

            //Se ho validazioni fallite
            if (validations.Count > 0)
                return BadRequest(validations);

            //Se è tutto ok, genero il contratto
            return Ok(ContractUtils.GenerateContract(entity));
        }

        /// <summary>
        /// Get single audience data
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Returns action result</returns>
        [HttpPost]
        [Route("GetAudience")]
        public IActionResult GetAudience([FromBody]AudienceRequest request)
        {
            //Validazione argomenti
            if (request == null)
                return BadRequest();

            //Se non è valida
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Recupero l'utente per username
            User user = Layer.GetUserByUserName(User.Identity.Name);
            if (user == null)
                return NotFound();

            //Se l'utente non ha diritti amministrativi, esco
            if (!Layer.HasAdministrativeGrants(user))
                return Unauthorized();

            //Get single element from storage
            var single = Layer.GetAudienceByClientId(request.ClientId);
            if (single == null)
                return NotFound();

            //Creo il contratto ed emetto
            var contract = ContractUtils.GenerateContract(single);
            return Ok(contract);
        }

        /// <summary>
        /// Fetch list of audiences
        /// </summary>
        /// <returns>Returns action result</returns>
        [HttpPost]
        [Route("FetchAudiences")]
        public IActionResult FetchAudiences()
        {
            //Recupero l'utente per username
            User user = Layer.GetUserByUserName(User.Identity.Name);
            if (user == null)
                return NotFound();

            //Se l'utente non ha diritti amministrativi, esco
            if (!Layer.HasAdministrativeGrants(user))
                return Unauthorized();

            //Recupero l'elenco dei token
            var entities = Layer.FetchAudiences();

            //Genero i contratti di uscita e li invio
            IList<AudienceContract> contracts = new List<AudienceContract>();
            entities.Each(e => contracts.Add(ContractUtils.GenerateContract(e)));
            return Ok(contracts);
        }

        /// <summary>
        /// Fetch list of refresh tokens on platform
        /// </summary>
        /// <returns>Returns action result</returns>
        [HttpPost]
        [Route("FetchRefreshTokens")]
        public IActionResult FetchRefreshTokens()
        {
            //Recupero l'utente per username
            User user = Layer.GetUserByUserName(User.Identity.Name);
            if (user == null)
                return NotFound();

            //Se l'utente non ha diritti amministrativi, esco
            if (!Layer.HasAdministrativeGrants(user))
                return Unauthorized();

            //Recupero l'elenco dei token
            var entities = Layer.FetchRefreshTokens();

            //Genero i contratti di uscita e li invio
            IList<RefreshTokenContract> contracts = new List<RefreshTokenContract>();
            entities.Each(e => contracts.Add(ContractUtils.GenerateContract(e)));
            return Ok(contracts);
        }

        /// <summary>
        /// Deletes a refresh token using provided request
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Returns action result</returns>
        [HttpPost]
        [Route("DeleteRefreshToken")]
        public IActionResult DeleteRefreshToken([FromBody]RefreshTokenRequest request)
        {
            //Validazione argomenti
            if (request == null)
                return BadRequest();

            //Se il modello non è valido
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Recupero l'utente per username
            User user = Layer.GetUserByUserName(User.Identity.Name);
            if (user == null)
                return NotFound();

            //Se l'utente non ha diritti amministrativi, esco
            if (!Layer.HasAdministrativeGrants(user))
                return Unauthorized();

            //Tento il recupero del token usando l'hash
            RefreshToken entity = Layer.GetRefreshToken(request.TokenHash);
            if (entity == null)
                return NotFound();

            //Eseguo la cancellazione e confermo
            Layer.DeleteRefreshToken(entity);
            return Ok();
        }
    }
}
