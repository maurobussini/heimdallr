using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Chakra.Core.Configurations;
using ZenProgramming.Heimdallr.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ZenProgramming.Heimdallr.Api.Providers
{
    /// <summary>
    /// Extensions for service collection for JWT token generation
    /// </summary>
    public static class JwtAuthorizationExtensions
    {
        /// <summary>
        /// Add JWT authorization to pipeline for authenticated incoming requests
        /// </summary>
        /// <param name="services">List of services</param>
        /// <returns>Returns list of services</returns>
        public static IServiceCollection AddJwtAuthorization(this IServiceCollection services)
        {
            //Validazione argomenti
            if (services == null) throw new ArgumentNullException(nameof(services));

            //Creo una chiave simmetrica di cifratura sulla base del 
            //client secret impostato per l'applicazione corrente
            var clientSecret = ConfigurationFactory<HeimdallrConfiguration>.Instance.Platform.AudienceClientSecret;
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(clientSecret));

            //Creazione dei parametri di validazione del token
            var tokenValidationParameters = new TokenValidationParameters
            {
                //Abilitazione della validazione sulla base della chiave di sicurezza
                ValidateIssuerSigningKey = true,                

                //Impostazione delle chiavi di sicurezza (solo quella generata)
                IssuerSigningKeys = new List<SecurityKey> { signingKey },

                //Disabilitazione della validazione dell'audience e issuer
                ValidateAudience = false,
                ValidateIssuer = false,


                //Disabilitazione della validazione della scadenza del token
                ValidateLifetime = false,

                //Nessuna tolleranza tra orario server ed emissione (stesso server)
                ClockSkew = TimeSpan.Zero
            };


            //Aggiunta dell'autenticazione
            services
                .AddAuthentication(options =>
                {
                    //Impostazione dello schema di base di autenticazione e challenge
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {

                    //Inclusione degli errori nel dettaglio (in Development)
                    o.IncludeErrorDetails = true;

                    //Applicazione dei parametri di validazione del token
                    o.TokenValidationParameters = tokenValidationParameters;

                    //Aggiunta della gestione degli eventi
                    o.Events = new JwtBearerEvents
                    {
                        //In caso di validazione fallita
                        OnAuthenticationFailed = c =>
                        {
                            //Nessuna emissione del risultato
                            c.NoResult();

                            //Response con 401 in text/plain
                            c.Response.StatusCode = 401;
                            c.Response.ContentType = "text/plain";

                            //Tracciamento del fallimento di autenticazione (per monitoring)
                            Debug.WriteLine("Failed to authenticate");
                            
                            //Scrittura della response dell'eccezione
                            return c.Response.WriteAsync(c.Exception.ToString());
                        }
                    };
                });

            //Ritorno i servizi
            return services;
        }
    }
}
