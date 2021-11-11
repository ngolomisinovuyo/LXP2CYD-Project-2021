using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace LXP2CYD.Controllers
{
    public abstract class LXP2CYDControllerBase: AbpController
    {
        protected LXP2CYDControllerBase()
        {
            LocalizationSourceName = LXP2CYDConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
