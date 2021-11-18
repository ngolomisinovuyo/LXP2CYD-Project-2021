using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using LXP2CYD.Authorization;
using LXP2CYD.Controllers;
using LXP2CYD.Programmes;
using LXP2CYD.Programmes.Dtos;
using LXP2CYD.Users;
using LXP2CYD.Users.Dto;
using LXP2CYD.Web.Models.Programmes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LXP2CYD.Web.Controllers
{
    [AbpMvcAuthorize()]
    public class ProgrammesController : LXP2CYDControllerBase
    {
        private readonly IProgrammeAppService _programmeAppService;
        private readonly IUserAppService _userAppService;
        public ProgrammesController(IProgrammeAppService programmeAppService,
            IUserAppService userAppService)
        {
            _programmeAppService = programmeAppService;
            _userAppService = userAppService;
        }
        // GET: CentersController
        public async Task<IActionResult> Index()
        {
            var result = await _userAppService.GetAllAsync(new PagedUserResultRequestDto
            {
                IsActive = true,
            });
            var programmeResults = await _programmeAppService.GetAllAsync(new PagedProgrammeResultRequestDto
            {
                IsActive = true,
            });
            var model = new ProgrammeListViewModel
            {
                Programmes = programmeResults.Items,
               Users = result.Items
            };
            return View(model);
        }

        // GET: CentersController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var programmeDto = await _programmeAppService.GetAsync(new EntityDto<int>(id));
            var users = await _userAppService.GetAllAsync(new PagedUserResultRequestDto
            {
                IsActive = true,
            });
            var model = new EditProgrammeModalViewModel
            {
                Programme = programmeDto,
                Users = users.Items
            };
            return View(model);
        }

        // GET: CentersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CentersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CentersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CentersController/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProgrammeDto programmeDto)
        {
            try
            {
                await _programmeAppService.UpdateAsync(programmeDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CentersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CentersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> EditModal(int id)
        {
            var programmeDto = await _programmeAppService.GetAsync(new EntityDto<int>(id));
            var users = await _userAppService.GetAllAsync(new PagedUserResultRequestDto
            {
                IsActive = true,
            });
            var model = new EditProgrammeModalViewModel
            {
                Programme = programmeDto,
                Users = users.Items
            };

            return PartialView("_EditModal", model);
        }

    }
}
