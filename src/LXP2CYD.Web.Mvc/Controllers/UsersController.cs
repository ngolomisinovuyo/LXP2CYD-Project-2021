using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using LXP2CYD.Authorization;
using LXP2CYD.Controllers;
using LXP2CYD.Users;
using LXP2CYD.Web.Models.Users;

namespace LXP2CYD.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Users)]
    public class UsersController : LXP2CYDControllerBase
    {
        private readonly IUserAppService _userAppService;

        public UsersController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public async Task<ActionResult> Index()
        {
            var roles = (await _userAppService.GetRoles()).Items;
            var regions = await _userAppService.GetRegions();
            var provinces = await _userAppService.GetProvinces();
            var model = new UserListViewModel
            {
                Provinces = provinces,
                Regions = regions,
                Roles = roles
            };
            return View(model);
        }

        public async Task<ActionResult> EditModal(long userId)
        {
            var user = await _userAppService.GetAsync(new EntityDto<long>(userId));
            var roles = (await _userAppService.GetRoles()).Items;
            var regions = await _userAppService.GetRegions();
            var provinces = await _userAppService.GetProvinces();
            var model = new EditUserModalViewModel
            {
                User = user,
                Provinces = provinces,
                Regions = regions,
                Roles = roles
            };
            return PartialView("_EditModal", model);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
    }
}
