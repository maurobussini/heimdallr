using ZenProgramming.Heimdallr.Data.Repositories;
using ZenProgramming.Heimdallr.Entities;
using ZenProgramming.Heimdallr.Mocks.Scenarios.Common;
using ZenProgramming.Chakra.Core.Data;
using ZenProgramming.Chakra.Core.Data.Repositories.Attributes;
using ZenProgramming.Chakra.Core.Data.Repositories.Mockups;

namespace ZenProgramming.Heimdallr.Mocks.Data.Repositories
{
    /// <summary>
    /// Mock implementation for repository of "Audience"
    /// </summary>
    [Repository]
    public class MockAudienceRepository:  MockupRepositoryBase<Audience, IHeimdallrScenario>, IAudienceRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataSession"></param>
        public MockAudienceRepository(IDataSession dataSession) 
            : base(dataSession, scn => scn.Audiences) { }        
    }
}
