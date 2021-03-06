using System;
using Abp.Application.Services.Dto;

namespace LXP2CYD.YearPlans.Dtos
{
    public class YearPlanDto: EntityDto<int>
    {
        public int Year { get; set; }
        public int? TenantId { get; set; }
        public Status Status { get; set; }
        public string Descrioption { get; set; }
    }
}
