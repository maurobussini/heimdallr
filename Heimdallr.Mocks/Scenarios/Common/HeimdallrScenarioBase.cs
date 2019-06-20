using System.Collections.Generic;
using ZenProgramming.Heimdallr.Entities;

namespace ZenProgramming.Heimdallr.Mocks.Scenarios.Common
{
    /// <summary>
    /// Base class for application scenarios
    /// </summary>
    public abstract class HeimdallrScenarioBase: IHeimdallrScenario
    {
        /// <summary>
        /// Users
        /// </summary>
        public IList<User> Users { get; set; }

        /// <summary>
        /// Audiences
        /// </summary>
        public IList<Audience> Audiences { get; set; }

        /// <summary>
        /// Refresh tokens
        /// </summary>
        public IList<RefreshToken> RefreshTokens { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected HeimdallrScenarioBase()
        {
            //Inizializzo le liste
            Users = new List<User>();
            Audiences = new List<Audience>();
            RefreshTokens = new List<RefreshToken>();
        }

        /// <summary>
        /// Initialize entities for scenario
        /// </summary>
        public abstract void InitializeEntities();

        /// <summary>
        /// Initialize assets
        /// </summary>
        public void InitializeAssets()
        {
            //Non richiesto per questo applicativo
        }
    }
}
