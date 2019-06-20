using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Chakra.Core.Configurations;
using ZenProgramming.Heimdallr.Configurations;
using ZenProgramming.Heimdallr.Entities;
using Microsoft.IdentityModel.Tokens;

namespace ZenProgramming.Heimdallr.Api.Helpers
{
    /// <summary>
    /// Utilities for manage JWT tokens
    /// </summary>
    public static class JwtUtils
    {
        /// <summary>
        /// Generates JWT security token based on provided audience and user
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="audience">Audience</param>
        /// <returns>Returns security</returns>
        public static JwtSecurityToken GenerateJwtSecurityToken(User user, Audience audience)
        {
            //Validazinne argomenti
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (audience == null) throw new ArgumentNullException(nameof(audience));

            //var userClaims = await _userManager.GetClaimsAsync(user);
            IEnumerable<Claim> userClaims = new List<Claim>();

            //Recupero i bytes del client secret dell'audience
            var bytes = Encoding.UTF8.GetBytes(audience.ClientSecret);

            //Creo la chiave simmetrica con algoritmo H-256 e le crendenziali
            var symmetricKey = new SymmetricSecurityKey(bytes);
            var credentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

            //Scadenza del token tra 30 minuti
            var expires = DateTime.UtcNow.AddMinutes(30);

            //Creo il JWT token
            return new JwtSecurityToken(
                issuer: ConfigurationFactory<HeimdallrConfiguration>.Instance.Platform.Issuer,
                audience: audience.ClientId,
                claims: GetTokenClaims(user).Union(userClaims),
                expires: expires,
                signingCredentials: credentials
            );
        }

        /// <summary>
        /// Get claims for provided user 
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>Returns claims</returns>
        private static IEnumerable<Claim> GetTokenClaims(User user)
        {
            //Validazinne argomenti
            if (user == null) throw new ArgumentNullException(nameof(user));

            //Compongo il payload con tutti i dati da trasmettere
            return new List<Claim>
            {                
                //new Claim(ClaimTypes.Name, user.UserName),
                //new Claim(ClaimTypes.GivenName, user.PersonName),
                //new Claim(ClaimTypes.Surname, user.PersonSurname),
                //new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.GivenName, user.PersonName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.PersonSurname),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
        }
    }
}
