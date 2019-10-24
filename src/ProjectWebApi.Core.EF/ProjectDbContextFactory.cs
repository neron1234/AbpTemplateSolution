using ProjectWebApi.Core.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ProjectWebApi.Core.EF
{
    public class GsoDbContextFactory : IDesignTimeDbContextFactory<ProjectDbContext>
    {
        public ProjectDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ProjectDbContext>();
            var configuration = AppConfiguration.Get(WebContentDirectoryFinder.CalculateContentRootFolder(), "Development");
            
            DbContextOptionsConfigurer.Configure(builder, configuration.GetConnectionString("DefaultConnection"));

            return new ProjectDbContext(builder.Options);
        }
    }
}