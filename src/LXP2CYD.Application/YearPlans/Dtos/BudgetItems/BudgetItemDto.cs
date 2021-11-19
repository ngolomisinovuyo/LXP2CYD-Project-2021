using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using LXP2CYD.YearPlans.Dtos.Items;

namespace LXP2CYD.YearPlans.Dtos.BudgetItems
{
    [AutoMapTo(typeof(BudgetItem))]
    [AutoMapFrom(typeof(BudgetItem))]
    public class BudgetItemDto: EntityDto<int>
    {
        public int? TenantId { get; set; }
        public string Name { get; set; }
        public double BudgtedAmount { get; set; }
        public double AmountSpent { get; set; }
        public int ItemId { get; set; }
        public int? YearPlanId { get; set; }
        public ItemDto Item { get; set; }
        public YearPlanDto YearPlan { get; set; }
    }
}
