using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ZenProgramming.Heimdallr.ServiceLayers;
using Microsoft.AspNetCore.Mvc;
using ZenProgramming.Chakra.Core.Data;

namespace ZenProgramming.Heimdallr.Api.Controllers.Common
{
    /// <summary>
    /// Base controller for MVC pattern
    /// </summary>
    public abstract class ApiControllerBase: Controller
    {
        #region Protected properties
        /// <summary>
        /// Session holder
        /// </summary>
        protected IDataSession DataSession { get; }

        /// <summary>
        /// Identity service layer instance
        /// </summary>
        protected IdentityServiceLayer Layer;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        protected ApiControllerBase()
        {
            //Inizializzo la session e il dominio
            DataSession = SessionFactory.OpenSession();
            Layer = new IdentityServiceLayer(DataSession);
        }

        /// <summary>
        /// Compose a BarRequest (400) with provided list of validations
        /// </summary>
        /// <param name="validations">Validations</param>
        /// <returns>Returns bad request response</returns>
        protected IActionResult BadRequest(IList<ValidationResult> validations)
        {
            //Validazione argomenti
            if (validations == null) throw new ArgumentNullException(nameof(validations));

            //Scorro tutti gli errori, inserisco nel modello ed esco
            foreach (var current in validations)
                ModelState.AddModelError("", current.ErrorMessage);

            //Ritorno la request
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, 
        /// releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="isDisposing">Explicit dispose</param>
        protected new virtual void Dispose(bool isDisposing)
        {
            //Se sto facendo la dispose
            if (isDisposing)
            {
                //Rilascio i layers e la sessione
                Layer?.Dispose();
                DataSession?.Dispose();
            }

            //Chiamo il metodo base
            base.Dispose(isDisposing);
        }
    }
}
