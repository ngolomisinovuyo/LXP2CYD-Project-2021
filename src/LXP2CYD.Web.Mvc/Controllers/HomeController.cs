using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using LXP2CYD.Controllers;

namespace LXP2CYD.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : LXP2CYDControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
