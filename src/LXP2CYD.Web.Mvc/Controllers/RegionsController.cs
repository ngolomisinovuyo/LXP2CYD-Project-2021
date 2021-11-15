using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using AutoMapper;
using LXP2CYD.Authorization;
using LXP2CYD.Controllers;
using LXP2CYD.Settings.Provinces;
using LXP2CYD.Settings.Regions;
using LXP2CYD.Settings.Regions.Dto;
using LXP2CYD.Web.Models.Settings.Regions;
using Microsoft.AspNetCore.Mvc;

namespace LXP2CYD.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Centers)]
    public class RegionsController: LXP2CYDControllerBase
    {
        private readonly IRegionAppService _regionAppService;

        public RegionsController(IRegionAppService regionAppService
            )
        {
            _regionAppService = regionAppService;
            
        }
        public async Task<ActionResult> Index()
        {
            var provinces = await _regionAppService.GetProvinces();
            var model = new RegionListViewModel
            {
                Provinces = provinces
            };
            return View(model);
        }
        public async Task<ActionResult> EditModal(int regionId)
        {
            var regionDto = await _regionAppService.GetAsync(new EntityDto<int>(regionId));
            var provinces = await _regionAppService.GetProvinces();
            var model = new EditRegionModalViewModel
            {
                Region = regionDto,
                Provinces = provinces
            };

            return PartialView("_EditModal", model);
        }
    }
}
