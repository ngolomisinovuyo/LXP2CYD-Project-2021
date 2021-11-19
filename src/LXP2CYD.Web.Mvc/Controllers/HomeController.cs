using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using LXP2CYD.Controllers;
using Abp.Domain.Repositories;
using LXP2CYD.Settings.Regions;
using LXP2CYD.MultiTenancy;
using LXP2CYD.LearnerModels.Learners;
using LXP2CYD.Authorization.Users.Staffs;
using LXP2CYD.Web.Models.Home;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LXP2CYD.Authorization.Users;
using System.Linq;
using LXP2CYD.Users.Dto;
using System;
using System.Collections.Generic;

namespace LXP2CYD.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : LXP2CYDControllerBase
    {
        private readonly IRepository<Region, int> _regionRepository;
        private readonly IRepository<Tenant, int> _tenantRepository;
        private readonly IRepository<Learner, long> _learnerRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<Staff, long> _staffRepository;
        public HomeController(IRepository<Region, int> regionRepository,
            IRepository<Tenant, int> tenantRepository,
             IRepository<Learner, long> learnerRepository,
             IRepository<Staff, long> staffRepository,
             IRepository<User, long> userRepository)
        {
            _regionRepository = regionRepository;
            _tenantRepository = tenantRepository;
            _learnerRepository = learnerRepository;
            _staffRepository = staffRepository;
            _userRepository = userRepository;
        }
        public async Task<IActionResult> Index()
        {
            var regionsCount = await _regionRepository.CountAsync();
            var centersCount = await _tenantRepository.CountAsync();
            var mentorsCount = await _staffRepository.CountAsync();
            var learnersCount = await _learnerRepository.CountAsync();

            var learners = await _userRepository.GetAll().Include(x => x.Learner).Where(x => x.Learner != null)
                .Select(x => new UserDto
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    CreationTime = x.CreationTime,
                    EmailAddress = x.EmailAddress,
                    PhoneNumber = x.PhoneNumber,
                })
                .Take(5)
                .ToListAsync();
            var model = new DashboardViewModel
            {
                RegionsCount = regionsCount,
                CenterCount = centersCount,
                LearnersCount = learnersCount,
                StaffCount = mentorsCount,
                Users = learners
            };
            return View(model);
        }
        public async Task<IActionResult> GetChartData()
        {
            string[] monthNames =
            System.Globalization.CultureInfo.CurrentCulture
                .DateTimeFormat.MonthGenitiveNames;

            int[] months = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            int year = DateTime.Now.Year;
            var dataSet = new List<CartDataModel>();
            var learners = await _learnerRepository.GetAll().Where(x=>x.CreationTime.Year == year).ToListAsync();
            foreach(int month in months)
            {
                dataSet.Add(new CartDataModel
                {
                    Month = month,
                    Count = learners.Count(x => x.CreationTime.Month == month)
                });
            }

            return Json(dataSet);

        }
    }
}
