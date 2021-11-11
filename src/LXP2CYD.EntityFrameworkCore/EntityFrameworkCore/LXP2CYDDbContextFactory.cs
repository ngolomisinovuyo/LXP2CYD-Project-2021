using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using LXP2CYD.Configuration;
using LXP2CYD.Web;

namespace LXP2CYD.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class LXP2CYDDbContextFactory : IDesignTimeDbContextFactory<LXP2CYDDbContext>
    {
        public LXP2CYDDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<LXP2CYDDbContext>();
            
            /*
             You can provide an environmentName parameter to the AppConfigurations.Get method. 
             In this case, AppConfigurations will try to read appsettings.{environmentName}.json.
             Use Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") method or from string[] args to get environment if necessary.
             https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli#args
             */
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            LXP2CYDDbContextConfigurer.Configure(builder, configuration.GetConnectionString(LXP2CYDConsts.ConnectionStringName));

            return new LXP2CYDDbContext(builder.Options);
        }
    }
}
