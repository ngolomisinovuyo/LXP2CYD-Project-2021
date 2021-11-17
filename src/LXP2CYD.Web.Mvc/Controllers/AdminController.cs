using Abp.Domain.Repositories;
using AutoMapper.Configuration;
using LXP2CYD.Admin;
using LXP2CYD.Authorization.Accounts;
using LXP2CYD.Authorization.Users;
using LXP2CYD.Controllers;
using LXP2CYD.Email;
using LXP2CYD.LearnerModels.Learners;
using LXP2CYD.MultiTenancy;
using LXP2CYD.Programmes.Dtos;
using LXP2CYD.Users;
using LXP2CYD.Web.Models.Admin;
using LXP2CYD.Web.Models.Programmes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LXP2CYD.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminAppService _adminAppService;
        public AdminController(IAdminAppService adminAppService)
        {
            _adminAppService = adminAppService;
        }
        public IActionResult Index()
        {
            return View();
        }
        //Certificate methods
        public async Task<IActionResult> CertificateOfParticipation(int id, long userId)
        {

            if (id == 0)
                return RedirectToAction(nameof(ErrorPage), new { message = "The resource you are trying to access is currently unavailable" });

            CertificateViewModel model = new CertificateViewModel();

            model.CenterDetails =await _adminAppService.GetTenant();
            model.Learner = await _adminAppService.GetLearner(userId);
            model.Programme =await _adminAppService.GetProgramme(id);

            CertificatePdf(model);
            return View(model);
        }
        public IActionResult CertificatePdf(CertificateViewModel model)
        {
            return View(model);
        }
        public async Task<IActionResult> PrintCertificate(int id)
        {
            CertificateViewModel model = new CertificateViewModel();

            model.CenterDetails = await _adminAppService.GetTenant();
            model.Learner = await _adminAppService.GetLearner();
            model.Programme = await _adminAppService.GetProgramme(id);

            return new ViewAsPdf("CertificatePdf", model);
        }
        //Deregister for a programme
        public async Task<IActionResult> Deregister(int id)
        {
            if (id == 0)
            {
                return RedirectToAction(nameof(ErrorPage), new { message = "The resource you are trying to access is currently unavailable" });
            }

            var record = await _adminAppService.GetProgrammeReservation(id);

            if (record == null)
            {
                return RedirectToAction(nameof(ErrorPage), new { message = "The resource you are trying to access is currently unavailable" });
            }

            try
            {
                await _adminAppService.DeleteProgrammeReservation(id);
                // return RedirectToAction(nameof(ViewPrograms));
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(ErrorPage), new { message = "The resource you are trying to access is currently unavailable" });

            }
        }
        public async Task<IActionResult> PostFeedback(int id)
        {
            LearnerProgrammeViewModel results = new LearnerProgrammeViewModel();
            results.Programme = await _adminAppService.GetProgramme(id);

            var userId =await _adminAppService.GetUserId();

            results.ProgrammeReservation = await _adminAppService.GetProgrammeReservationByUserAndProgrammeId(id, userId);
            return View(results);
        }
        [HttpPost]
        public async Task<IActionResult> PostFeedback(LearnerProgrammeViewModel model)
        {
            var programmeReservation = await _adminAppService.GetProgrammeReservation(model.ProgrammeReservation.Id);

            //if (programmeReservation == null)
            //    return RedirectToAction(nameof(ErrorPage));

            //var record = new EventReservations()
            //{
            //    ReservationId = model.rsvp.ReservationId,
            //    attended = result.attended,
            //    Feedback = model.rsvp.Feedback,
            //    ProgramId = model.rsvp.ProgramId,
            //    UserId = model.rsvp.UserId,
            //};

            try
            {
                await _adminAppService.UpdateProgrammeReservation(programmeReservation);
                return RedirectToAction(nameof(PostFeedback));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(ErrorPage), new { message = "The resource you are trying to access is currently unavailable" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Enroll(int id, long userId)
        {
            if (id == 0 || userId == null)
                return RedirectToAction(nameof(ViewPrograms), new { isSuccess = false });

            var model = new CreateProgrammeReservationDto()
            {
                ProgrammeId = id,
                UserId = userId
            };

            try
            {
                var programmeReservation = await _adminAppService.GetProgrammeReservationByUserAndProgrammeId(id, userId);

                if (programmeReservation != null)
                {
                    return RedirectToAction(nameof(ViewPrograms), new { message = "You are already enrolled for this programme" });
                }
                await _adminAppService.CreateProgrammeReservation(model);
                return RedirectToAction(nameof(ViewPrograms), new { isSuccess = true });
            }
            catch (Exception c)
            {
                return RedirectToAction(nameof(ErrorPage), new { message = "The resource you are trying to access is currently unavailable" });
            }

        }

        public async Task<IActionResult> ViewPrograms(string message, bool isSuccess)
        {
            LearnerProgrammeViewModel model = new LearnerProgrammeViewModel();
            var id =await _adminAppService.GetUserId();
            try
            {
                model.Programmes = (await _adminAppService.GetProgrammes(new PagedProgrammeResultRequestDto { SkipCount = 0, MaxResultCount = 10, })).Items;
                model.ProgrammeReservations = await _adminAppService.GetProgrammeReservationsByUserId(id);
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(ErrorPage), new { message = e.ToString() });
            }

            ViewBag.error = message;
            ViewBag.isSuccess = isSuccess;

            return View(model);
        }

        //<------Error page ------>
        public IActionResult ErrorPage(string message) => View();
      
 
        public async Task<IActionResult> AddAboutInfo()
        {
            //ViewBag.ProvinceList = new SelectList(_addressRepository.GetProvinceListAsync(), "ProvinceId", "ProvinceName");
            var details = _adminAppService.GetTenant();

            return View(details);
        }
       
        //Admin Program methods
        public IActionResult CreateProgram() => View();

        //[HttpPost]
        //public async Task<IActionResult> CreateProgram(CreateProgrammeDto model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            //_context.Programmes.Add(model);
        //            //await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(ListAllPrograms), new { IsSuccess = true });
        //        }
        //        catch (Exception c)
        //        {
        //            return RedirectToAction("ErrorPage", "Admin");
        //        }
        //    }
        //    return View(model);
        //}

        public async Task<IActionResult> EditProgram(int id)
        {
            if (id == 0)
                return RedirectToAction(nameof(ErrorPage));

            var results = await _adminAppService.GetProgramme(id);
            if (results == null)
                return RedirectToAction(nameof(ErrorPage));

            return View(results);
        }

        [HttpPost]
        public async Task<IActionResult> EditProgram(ProgrammeDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Programmes.Update(model);
                    //await _context.SaveChangesAsync();
                    //return RedirectToAction(nameof(ListAllPrograms), new { IsSuccess = true });
                }
                catch (Exception c)
                {
                    RedirectToAction(nameof(ErrorPage));
                }
            }
            return View(model);
        }

        //[HttpPost]
        //public async Task<IActionResult> DeleteProgram(int id)
        //{
        //    if (id == 0)
        //        return RedirectToAction(nameof(ErrorPage));

        //    var results = _context.Programmes.FirstOrDefault(i => i.Id == id);
        //    if (results == null)
        //        return RedirectToAction(nameof(ErrorPage));

        //    try
        //    {
        //        _context.Programmes.Remove(results);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(ListAllPrograms), new { IsSuccess = true });
        //    }
        //    catch (Exception c)
        //    {
        //        RedirectToAction(nameof(ErrorPage));
        //    }

        //    return RedirectToAction(nameof(ListAllPrograms));
        //}

        //public IActionResult EndUserFeedback(string message)
        //{
        //    ViewBag.Message = message;
        //    var results = _context.Enquiries.ToList();
        //    return View(results);
        //}

        public IActionResult SendUserFeedback(int id, string message)
        {
            if (id == 0)
                return RedirectToAction(nameof(ErrorPage));

            //var model = _context.Enquiries.FirstOrDefault(w => w.Id == id);

            //if (model == null)
            //    return RedirectToAction(nameof(ErrorPage));
            //ViewBag.message = message;
            //EmailEnquiryResponse response = new EmailEnquiryResponse
            //{
            //    Name = model.FirstName + " " + model.LastName,
            //    userEmail = model.EmailAddress
            //};
            //return View(response);
            return View();
        }


        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //public IActionResult SendUserFeedback(EmailEnquiryResponse model)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        var results = SendResponseEmail(model);
        //        string message = "Response has been sent Successfully";
        //        return RedirectToAction(nameof(EndUserFeedback), new { message = message });
        //    }
        //    return View(model);
        //}
        //private async Task SendResponseEmail(EmailEnquiryResponse model)
        //{
        //    string appDomain = _config.GetSection("Application:AppDomain").Value;
        //    string confirmationLink = _config.GetSection("Application:EmailConfirmation").Value;

        //    UserEmailOptions options = new UserEmailOptions
        //    {
        //        ToEmails = new List<string> { model.userEmail },
        //        PlaceHolders = new List<KeyValuePair<string, string>>()
        //        {
        //            new KeyValuePair<string, string>("{{UserName}}", model.Name),
        //             new KeyValuePair<string, string>("{{message}}", model.body),
        //        }
        //    };
        //    await _emailService.SendEqnuiryResponseEmail(options);
        //}
    }
}
