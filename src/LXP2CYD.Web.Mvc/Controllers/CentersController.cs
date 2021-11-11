using Abp.AspNetCore.Mvc.Authorization;
using LXP2CYD.Authorization;
using LXP2CYD.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LXP2CYD.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Centers)]
    public class CentersController : LXP2CYDControllerBase

    {
        public CentersController()
        {

        }
        // GET: CentersController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CentersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
    }
}
