using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace LXP2CYD.YearPlans
{
    [Table("AppItems")]
    public class Item : FullAuditedEntity<int>, IMayHaveTenant
    {
        public int? TenantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int YearPlanId { get; set; }
        [ForeignKey(nameof(YearPlanId))]
        public YearPlan YearPlan { get; set; }
    }
}
