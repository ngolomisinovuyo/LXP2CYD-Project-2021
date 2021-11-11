using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using LXP2CYD.Authorization;

namespace LXP2CYD
{
    [DependsOn(
        typeof(LXP2CYDCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class LXP2CYDApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<LXP2CYDAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(LXP2CYDApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
