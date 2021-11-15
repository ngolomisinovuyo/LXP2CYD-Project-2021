using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using LXP2CYD.LearnerModels.Subjects;

namespace LXP2CYD.LearnerModels.Enrollments
{
    [Table("AppEnrollments")]
    public class Enrollment : FullAuditedEntity<int>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public int StaffSubjectId { get; set; }
        public int LearnerSubjectId { get; set; }
        [ForeignKey(nameof(LearnerSubjectId))]
        public LearnerSubject LearnerSubject { get; set; }
        [ForeignKey(nameof(StaffSubjectId))]
        public StaffSubject StaffSubject { get; set; }
    }
}
