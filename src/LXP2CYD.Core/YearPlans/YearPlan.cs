using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace LXP2CYD.YearPlans
{
    [Table("AppYearPlans")]
    public class YearPlan: FullAuditedEntity<int>, IMayHaveTenant
    {
        public int Year { get; set; }
        public int? TenantId { get; set; }
        public Status Status { get; set; }
        public string Descrioption { get; set; }
    }
}
