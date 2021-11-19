using System;
using Abp.Application.Services.Dto;

namespace LXP2CYD.YearPlans.Dtos
{
    public class PagedYearPlanResultRequestDto: PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
    }
}
