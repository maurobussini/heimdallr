using ZenProgramming.Heimdallr.Data.Repositories;
using ZenProgramming.Heimdallr.Entities;
using ZenProgramming.Heimdallr.EntityFramework.Contexts;
using ZenProgramming.Chakra.Core.Data;
using ZenProgramming.Chakra.Core.Data.Repositories.Attributes;
using ZenProgramming.Chakra.Core.EntityFramework.Data.Repositories;

namespace ZenProgramming.Heimdallr.EntityFramework.Data.Repositories
{
    /// <summary>
    /// Repository on EntityFramework engine for "Audience"
    /// </summary>
    [Repository]
    public class EfAudienceRepository : EntityFrameworkRepositoryBase<Audience, HeimdallrDbContext>, IAudienceRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataSession">Data session</param>
        public EfAudienceRepository(IDataSession dataSession) 
            : base(dataSession, dbc => dbc.Audiences) { }
    }
}
