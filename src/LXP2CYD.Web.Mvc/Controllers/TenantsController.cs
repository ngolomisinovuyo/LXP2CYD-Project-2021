using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using LXP2CYD.Authorization;
using LXP2CYD.Controllers;
using LXP2CYD.MultiTenancy;
using LXP2CYD.Web.Models.Tenants;

namespace LXP2CYD.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Tenants)]
    public class TenantsController : LXP2CYDControllerBase
    {
        private readonly ITenantAppService _tenantAppService;

        public TenantsController(ITenantAppService tenantAppService)
        {
            _tenantAppService = tenantAppService;
        }

        public async Task<ActionResult> Index()
        {
            var regions = await _tenantAppService.GetRegions();
            var model = new TenantListViewModel
            {
                Regions = regions
            };
            return View(model);
        }

        public async Task<ActionResult> EditModal(int tenantId)
        {
            var tenantDto = await _tenantAppService.GetAsync(new EntityDto(tenantId));
            var regions = await _tenantAppService.GetRegions();
            var model = new EditTenantModalViewModel
            {
                Tenant = tenantDto,
                Regions = regions
            };
            return PartialView("_EditModal", model);

        }
    }
}
