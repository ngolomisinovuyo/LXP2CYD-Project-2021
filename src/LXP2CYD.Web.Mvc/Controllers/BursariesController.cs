using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using AutoMapper;
using LXP2CYD.Authorization;
using LXP2CYD.Bursaries;
using LXP2CYD.Bursaries.Dtos;
using LXP2CYD.Controllers;
using LXP2CYD.Web.Models.Bursaries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LXP2CYD.Web.Controllers
{
    [AbpMvcAuthorize()]
    public class BursariesController : LXP2CYDControllerBase
    {
        private readonly IBursaryAppService _bursaryAppService;
        private readonly IMapper _mapper;
        public BursariesController(IBursaryAppService bursaryAppService, IMapper mapper)
        {
            _bursaryAppService = bursaryAppService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBursaryDto input)
        {
            await _bursaryAppService.CreateAsync(input);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> EditModal(int id)
        {
            var bursaryDto = await _bursaryAppService.GetAsync(new EntityDto<int>(id));
            var model = new EditBursaryModalViewModel
            {
                Bursary = bursaryDto
            };

            return PartialView("_EditModal", model);
        }

    }
}
