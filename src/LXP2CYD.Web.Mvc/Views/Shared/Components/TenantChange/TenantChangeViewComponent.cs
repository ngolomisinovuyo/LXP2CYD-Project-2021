using System.Threading.Tasks;
using Abp.ObjectMapping;
using LXP2CYD.Sessions;
using Microsoft.AspNetCore.Mvc;

namespace LXP2CYD.Web.Views.Shared.Components.TenantChange
{
    public class TenantChangeViewComponent : LXP2CYDViewComponent
    {
        private readonly ISessionAppService _sessionAppService;
        private readonly IObjectMapper _objectMapper;

        public TenantChangeViewComponent(ISessionAppService sessionAppService, IObjectMapper objectMapper)
        {
            _sessionAppService = sessionAppService;
            _objectMapper = objectMapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var loginInfo = await _sessionAppService.GetCurrentLoginInformations();
            var model = _objectMapper.Map<TenantChangeViewModel>(loginInfo);
            return View(model);
        }
    }
}
