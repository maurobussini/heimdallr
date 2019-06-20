using System;
using System.Linq;
using Chakra.Core.Configurations;
using ZenProgramming.Heimdallr.Configurations;
using ZenProgramming.Heimdallr.Entities;
using Microsoft.EntityFrameworkCore;

namespace ZenProgramming.Heimdallr.EntityFramework.Contexts
{
    /// <summary>
    /// DbContext for EntityFramework
    /// </summary>
    public class HeimdallrDbContext : DbContext
    {
        /// <summary>
        /// Users
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Audiences
        /// </summary>
        public DbSet<Audience> Audiences { get; set; }

        /// <summary>
        /// Refresh tokens
        /// </summary>
        public DbSet<RefreshToken> RefreshTokens { get; set; }


        //*** INSTRUCTIONS
        // Go on project folder where is located EntityFramework DbContext 
        // using command prompt (cmd or PowerShell) and type:
        //
        // > dotnet add package Microsoft.EntityFrameworkCore.Design
        // > dotnet restore
        //
        // If everything is installed correctly, you should see a confirm after
        // > dotnet ef
        //
        // Create an executable project (ex. .NET Core Console) that will be used 
        // as entry point for migrations (ex. Heimdallr.Terminal), then type:
        // 
        // > dotnet ef migrations add InitialMigration --startup-project ../Heimdallr.Terminal/Heimdallr.Terminal.csproj
        //
        // Generate SQL scripts using the following command: 
        //
        // > dotnet ef migrations script --startup-project ../Heimdallr.Terminal/Heimdallr.Terminal.csproj -o script.sql
        //
        // File "script.sql" will be generate on Heimdallr.Terminal folder


        /// <summary>
        /// Raised during context configuration
        /// </summary>
        /// <param name="optionsBuilder">Options for builder</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Arguments validation
            if (optionsBuilder == null) throw new ArgumentNullException(nameof(optionsBuilder));

            //Check is "Default connection string exists
            var defaultConnectionString = ConfigurationFactory<HeimdallrConfiguration>
                .Instance.ConnectionStrings
                .SingleOrDefault(c => c.Name == "Default");
            if (defaultConnectionString == null)
                throw new InvalidProgramException("Connection string named 'Default' cannot be found");

            //Add SQL configuration
            optionsBuilder.UseSqlServer(defaultConnectionString.ConnectionString);

            //Base configuration
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// Raised when model is going to be created
        /// </summary>
        /// <param name="modelBuilder">Model builder instance</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Mappo le entità
            modelBuilder.Entity<User>().ToTable("icHEIMDALLR_Users");
            modelBuilder.Entity<Audience>().ToTable("icHEIMDALLR_Audiences");
            modelBuilder.Entity<RefreshToken>().ToTable("icHEIMDALLR_RefreshTokens");            
        }
    }
}
