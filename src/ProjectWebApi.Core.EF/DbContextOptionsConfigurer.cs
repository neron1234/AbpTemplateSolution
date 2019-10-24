using Microsoft.EntityFrameworkCore;

namespace ProjectWebApi.Core.EF
{
    public static class DbContextOptionsConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ProjectDbContext> dbContextOptions, string connectionString)
        {
            dbContextOptions.EnableSensitiveDataLogging();
            dbContextOptions.UseNpgsql(connectionString, x => x.MigrationsAssembly("ProjectWebApi.Core.EF"));
        }
    }
}