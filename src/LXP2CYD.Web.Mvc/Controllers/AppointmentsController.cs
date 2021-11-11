using Abp.AspNetCore.Mvc.Authorization;
using LXP2CYD.Authorization;
using LXP2CYD.Controllers;
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
        public AppointmentsController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
