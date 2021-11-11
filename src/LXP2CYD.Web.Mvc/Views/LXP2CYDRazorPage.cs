using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace LXP2CYD.Web.Views
{
    public abstract class LXP2CYDRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected LXP2CYDRazorPage()
        {
            LocalizationSourceName = LXP2CYDConsts.LocalizationSourceName;
        }
    }
}
