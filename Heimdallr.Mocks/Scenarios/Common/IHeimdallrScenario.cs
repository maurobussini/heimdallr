using System.Collections.Generic;
using ZenProgramming.Heimdallr.Entities;
using ZenProgramming.Chakra.Core.Data.Mockups.Scenarios;

namespace ZenProgramming.Heimdallr.Mocks.Scenarios.Common
{
    /// <summary>
    /// Interface for application scenario
    /// </summary>
    public interface IHeimdallrScenario: IScenario
    {
        /// <summary>
        /// Users
        /// </summary>
        IList<User> Users { get; set; }

        /// <summary>
        /// Audiences
        /// </summary>
        IList<Audience> Audiences { get; set; }

        /// <summary>
        /// Refresh tokens
        /// </summary>
        IList<RefreshToken> RefreshTokens { get; set; }
    }
}
