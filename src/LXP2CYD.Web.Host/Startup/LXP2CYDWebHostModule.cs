using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using LXP2CYD.Configuration;

namespace LXP2CYD.Web.Host.Startup
{
    [DependsOn(
       typeof(LXP2CYDWebCoreModule))]
    public class LXP2CYDWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public LXP2CYDWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(LXP2CYDWebHostModule).GetAssembly());
        }
    }
}
