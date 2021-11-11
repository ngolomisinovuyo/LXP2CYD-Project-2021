using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using LXP2CYD.EntityFrameworkCore;
using LXP2CYD.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace LXP2CYD.Web.Tests
{
    [DependsOn(
        typeof(LXP2CYDWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class LXP2CYDWebTestModule : AbpModule
    {
        public LXP2CYDWebTestModule(LXP2CYDEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(LXP2CYDWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(LXP2CYDWebMvcModule).Assembly);
        }
    }
}