using System;
using System.Collections.Generic;
using LXP2CYD.YearPlans.Dtos;

namespace LXP2CYD.Web.Models.YearPlans
{
    public class YearPlanListViewModel
    {
        public IReadOnlyList<YearPlanDto> YearPlans { get; set; }
    }
}
