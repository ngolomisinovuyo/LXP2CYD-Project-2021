using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using LXP2CYD.LearnerModels.Schools;

namespace LXP2CYD.LearnerModels.Learners
{
    [Table("AppLearners")]
    public class Learner : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set ; }
        public long UserId { get; set; }
        public string IdNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Stream { get; set; }
        public int? SchoolId { get; set; }
        public Grade? Grade { get; set; }
        [ForeignKey(nameof(SchoolId))]
        public School School { get; set; }

    }
}
