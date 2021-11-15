using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using LXP2CYD.Authorization.Users.Staffs;

namespace LXP2CYD.LearnerModels.Subjects
{
    [Table("AppStaffSubjects")]
    public class StaffSubject : FullAuditedEntity<int>, IMustHaveTenant
    {
        public double? Rate { get; set; }
        public int TenantId { get; set; }
        public int SubjectId { get; set; }
        public long StaffId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; }

        [ForeignKey(nameof(StaffId))]
        public Staff Staff { get; set; }
    }
}
