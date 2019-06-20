using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Heimdallr.SampleResource.Api
{
    public class Startup
    {
        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Application name
        /// </summary>
        public string ApplicationName { get; }

        /// <summary>
        /// Application version
        /// </summary>
        public string ApplicationVersion { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">Injected configuration</param>
        public Startup(IConfiguration configuration)
        {
            //Assegno la configurazione locale
            Configuration = configuration;

            //Definizione del nome e versione del sistema
            ApplicationName = Assembly.GetEntryAssembly().GetName().Name;
            ApplicationVersion = $"v{Assembly.GetEntryAssembly().GetName().Version.Major}" +
                                 $".{Assembly.GetEntryAssembly().GetName().Version.Minor}" +
                                 $".{Assembly.GetEntryAssembly().GetName().Version.Build}";
        }

        /// <summary>
        /// Executes configuration of services
        /// </summary>
        /// <param name="services">Services</param>
        public void ConfigureServices(IServiceCollection services)
        {
            //Validazione argomenti
            if (services == null) throw new ArgumentNullException(nameof(services));

            //Abilitazione CORS
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services
            .AddAuthentication(cfg =>
            {
                //Impostazione del "Bearer" JWT come default per autenticazione e challenge
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg =>
            {
                //Client secret dell'applicazione corrente (censita su OAuth)
                var secretKey = "IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw";

                //Creazione dei parametri di validazione
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    //Validazione dell'issuer sull'indirizzo specificato (ATTENZIONE!
                    //questo verifica che l'indirizzo esista con il DNS resolvere)
                    ValidateIssuer = true,
                    ValidIssuer = "https://heimdallr-api.azurewebsites.net",

                    //Validazione dell'audience (usando il clientId censito in OAuth)
                    ValidateAudience = true,
                    ValidAudience = "heimdallr.api",

                    //Validazione del client secret sul token Bearer ottenuto
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                };
            });

            //Aggiungo MVC
            services.AddMvc();
        }

        /// <summary>
        /// Configures the HTTP request on runtime
        /// </summary>
        /// <param name="app">Application builder</param>
        /// <param name="env">Hosting environment</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //Validazione argomenti
            if (app == null) throw new ArgumentNullException(nameof(app));
            if (env == null) throw new ArgumentNullException(nameof(env));

            //Se siamo in modalità "dev"
            if (env.IsDevelopment())
            {
                //Abilito la pagina delle eccezioni
                app.UseDeveloperExceptionPage();
            }

            //Abilito CORS
            app.UseCors("CorsPolicy");

            //Utilizzo l'autenticazione
            app.UseAuthentication();

            //Utilizzo il pattern MVC
            app.UseMvc();
        }
    }
}
