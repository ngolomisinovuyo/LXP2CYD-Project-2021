using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Domain.Repositories;
using AutoMapper;
using LXP2CYD.Authorization;
using LXP2CYD.Controllers;
using LXP2CYD.Web.Models.YearPlans;
using LXP2CYD.YearPlans;
using LXP2CYD.YearPlans.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LXP2CYD.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Year_Plans)]
    public class YearPlansController: LXP2CYDControllerBase
    {
        private readonly IRepository<YearPlan, int> _yearPlanRepository;
        private readonly IMapper _mapper;
        public YearPlansController(IRepository<YearPlan, int> yearPlanRepository,
            IMapper mapper)
        {
            _yearPlanRepository = yearPlanRepository;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var yearPlans = await _yearPlanRepository.GetAllListAsync();
            var model = new YearPlanListViewModel
            {
                YearPlans = _mapper.Map<IReadOnlyList<YearPlanDto>>(yearPlans)
            };
            return View(model);
        }
    }

    
}
