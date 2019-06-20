using System;
using ZenProgramming.Heimdallr.Api.Models;
using ZenProgramming.Heimdallr.Entities;
using ZenProgramming.Heimdallr.Structures;

namespace ZenProgramming.Heimdallr.Api.Helpers
{
    /// <summary>
    /// Utilities for generate contract
    /// </summary>
    public class ContractUtils
    {
        /// <summary>
        /// Generates contract using provided entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Returns contract</returns>
        public static UserContract GenerateContract(User entity)
        {
            //Validazione argomenti
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            //Ritorno il contratto
            return new UserContract
            {
                UserName = entity.UserName, 
                PersonName = entity.PersonName,
                PersonSurname = entity.PersonSurname,
                Email = entity.Email,
                IsEnabled = entity.IsEnabled,
                IsLocked = entity.IsLocked,
                LastAccessDate = entity.LastAccessDate
            };
        }

        /// <summary>
        /// Generates contract using provided result
        /// </summary>
        /// <param name="signedInResult">Signed-in result</param>
        /// <returns>Return contract instance</returns>
        public static UserContract GenerateContract(SignInResult signedInResult)
        {
            //Validazione argomenti
            if (signedInResult == null) throw new ArgumentNullException(nameof(signedInResult));

            //Ritorno il contratto
            return new UserContract
            {
                UserName = signedInResult.UserName,
                PersonName = signedInResult.PersonName,
                PersonSurname = signedInResult.PersonSurname,
                Email = signedInResult.Email,
                IsEnabled = signedInResult.IsEnabled,
                IsLocked = signedInResult.IsLocked,
                LastAccessDate = signedInResult.LastAccessDate,
                SignInProvider = signedInResult.SignInProvider
            };
        }

        /// <summary>
        /// Generates contract using provided entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Returns contract</returns>
        public static RefreshTokenContract GenerateContract(RefreshToken entity)
        {
            //Validazione argomenti
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            //Ritorno il contratto
            return new RefreshTokenContract
            {
                TokenHash = entity.TokenHash,
                UserName = entity.UserName,
                ClientId = entity.ClientId,
                ExpiresUtc = entity.ExpiresUtc,
                IssuedUtc = entity.IssuedUtc
            };
        }

        /// <summary>
        /// Generates contract using provided entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Returns contract</returns>
        public static AudienceContract GenerateContract(Audience entity)
        {
            //Validazione argomenti
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            //Ritorno il contratto
            return new AudienceContract
            {
                AudienceId = entity.Id,
                Name = entity.Name,
                ClientId = entity.ClientId,
                ClientSecret = entity.ClientSecret,
                IsEnabled = entity.IsEnabled, 
                IsNative = entity.IsNative,
                AllowedOrigin = entity.AllowedOrigin,
                RefreshTokenLifeTime = entity.RefreshTokenLifeTime, 
                HasAdministrativeAccess = entity.HasAdministrativeAccess
            };
        }        
    }
}