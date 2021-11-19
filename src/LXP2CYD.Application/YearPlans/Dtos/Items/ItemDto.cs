using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace LXP2CYD.YearPlans.Dtos.Items
{
    [AutoMapFrom(typeof(Item))]
    [AutoMapTo(typeof(Item))]
    public class ItemDto: EntityDto<int>
    {
        public int? TenantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int YearPlanId { get; set; }
        public YearPlanDto YearPlan { get; set; }
    }
}
