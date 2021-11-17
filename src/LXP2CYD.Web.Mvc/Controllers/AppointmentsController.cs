using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using LXP2CYD.Appointments;
using LXP2CYD.Appointments.Dto;
using LXP2CYD.Authorization;
using LXP2CYD.Controllers;
using LXP2CYD.Web.Models.Appointment;
using LXP2CYD.Web.Models.Appointments;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LXP2CYD.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Appointments)]
    public class AppointmentsController : LXP2CYDControllerBase
    {
        private readonly IAppointmentAppService _appointmentAppService;
        private readonly IWebHostEnvironment _env;
        public AppointmentsController(IAppointmentAppService appointmentAppService,
            IWebHostEnvironment env)
        {
            _appointmentAppService = appointmentAppService;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _appointmentAppService.GetAttendees();
            var model = new AppointmentListViewModel
            {
                Users = users
            };
            return View(model);
        }
        public async Task<ActionResult> EditModal(int appointmentId)
        {
            var appointment = await _appointmentAppService.GetAsync(new EntityDto<int>(appointmentId));
            var users = await _appointmentAppService.GetAttendees();
            var model = new EditAppointmentModalViewModel
            {
                Users = users,
                Appointment = appointment
            };
            return PartialView("_EditModal", model);
        }
        public async Task<IActionResult> GetAppointments()
        {
            var input = new PagedAppointmentResultRequestDto
            {
                SkipCount = 0,
                MaxResultCount = 100,
                IsActive = true
            };
            var appointemts = await _appointmentAppService.GetAllAsync(input);
            return Json(appointemts.Items);
        }
        [HttpGet]
        public async Task<IActionResult> PrintAppointment(int id)
        {
            var appointment = await _appointmentAppService.GetAsync(new EntityDto<int>(id));
            if (appointment == default)
                return RedirectToAction(nameof(Index), new { IsSuccess = true });
            var dataDic = new ViewDataDictionary(ViewData) { { "Title", "Appointment" }, { "Layout", null } };
            return new ViewAsPdf("AppointmentPdf", new AppointmentPdf
            {
                AppointmentType = appointment.Type,
                CenterNotes = appointment.Notes,
                Date = appointment.StartTime,
                Time = appointment.StartTime,
                Status = appointment.Status,
                WebRootPath = _env.WebRootPath
            }, dataDic)
            {
                FileName = "Appointment_" + appointment.Id + ".pdf"
            };
        }
    }
}
