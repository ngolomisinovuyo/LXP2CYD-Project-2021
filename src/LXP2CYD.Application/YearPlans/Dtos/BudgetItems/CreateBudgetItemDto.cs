using System;
using Abp.AutoMapper;

namespace LXP2CYD.YearPlans.Dtos.BudgetItems
{
    [AutoMapTo(typeof(BudgetItem))]
    public class CreateBudgetItemDto
    {
        public string Name { get; set; }
        public double BudgtedAmount { get; set; }
        public double AmountSpent { get; set; }
        public int ItemId { get; set; }
        public int? YearPlanId { get; set; }
    }
}
