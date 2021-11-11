using Abp.AspNetCore.Mvc.ViewComponents;

namespace LXP2CYD.Web.Views
{
    public abstract class LXP2CYDViewComponent : AbpViewComponent
    {
        protected LXP2CYDViewComponent()
        {
            LocalizationSourceName = LXP2CYDConsts.LocalizationSourceName;
        }
    }
}
