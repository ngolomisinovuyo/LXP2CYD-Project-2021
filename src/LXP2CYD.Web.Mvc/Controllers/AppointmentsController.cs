using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using LXP2CYD.Appointments;
using LXP2CYD.Appointments.Dto;
using LXP2CYD.Authorization;
using LXP2CYD.Controllers;
using LXP2CYD.Web.Models.Appointment;
using Microsoft.AspNetCore.Mvc;
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
        public AppointmentsController(IAppointmentAppService appointmentAppService)
        {
            _appointmentAppService = appointmentAppService;
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
    }
}
