using Microsoft.EntityFrameworkCore;

namespace ZenProgramming.Heimdallr.EntityFramework.Initializers.Extensions
{
    public static class DbContextExtensions
    {
        public static void ApplyInitializer<TDbContext, TDbContextInitializer>(this TDbContext dbContextInstance)
            where TDbContext: DbContext
            where TDbContextInitializer: class, IDbContextInitializer<TDbContext>, new()
        {
            TDbContextInitializer instance = new TDbContextInitializer();
            instance.Seed(dbContextInstance);
        }
    }
}
