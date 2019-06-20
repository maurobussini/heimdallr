using System;
using System.IO;
using System.Reflection;
using ZenProgramming.Heimdallr.Api.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace ZenProgramming.Heimdallr.Api
{
    /// <summary>
    /// Application startup class
    /// </summary>
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

            //Aggiungo MVC
            services.AddMvc();

            //Aggiungo l'autorizzazione JWT
            services.AddJwtAuthorization();

            // This is for the [Authorize] attributes.
            services.AddAuthorization(auth => {
                auth.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });

            //Registro il generatore di Swagger
            services.AddSwaggerGen(c =>
            {
                //Informazioni di testata
                c.SwaggerDoc("v1", new Info { Title = ApplicationName, Version = ApplicationVersion });

                //Compongo il percorso del file XML
                string file = $"{typeof(Startup).Assembly.GetName().Name}.xml";
                string xmlPath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, file);

                //Includo i commenti XML
                c.IncludeXmlComments(xmlPath);
            });
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

            //Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{ApplicationName} {ApplicationVersion}");
            });

            //Utilizzo il pattern MVC
            app.UseMvc();
        }
    }
}
