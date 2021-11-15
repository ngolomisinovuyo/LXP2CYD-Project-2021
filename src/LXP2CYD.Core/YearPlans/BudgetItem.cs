using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace LXP2CYD.YearPlans
{
    [Table("AppBudgetItems")]
    public class BudgetItem : FullAuditedEntity<int>, IMayHaveTenant
    {
        public int? TenantId { get; set; }
        public string Name { get; set; }
        public double BudgtedAmount { get; set; }
        public double AmountSpent { get; set; }
        public int ItemId { get; set; }
        public int? YearPlanId { get; set; }
        [ForeignKey(nameof(ItemId))]
        public Item Item { get; set; }
        [ForeignKey(nameof(YearPlanId))]
        public YearPlan YearPlan { get; set; }
    }
}
