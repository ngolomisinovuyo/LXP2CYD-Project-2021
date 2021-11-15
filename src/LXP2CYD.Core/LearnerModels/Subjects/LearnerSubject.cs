using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using LXP2CYD.LearnerModels.Learners;

namespace LXP2CYD.LearnerModels.Subjects
{
    [Table("AppLearnerSubjects")]
    public class LearnerSubject: FullAuditedEntity<int>, IMustHaveTenant
    {
        public string GradeLevel { get; set; }
        public int TenantId { get; set; }
        public int SubjectId { get; set; }
        public long LearnerId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; }

        [ForeignKey(nameof(LearnerId))]
        public Learner Learner { get; set; }
    }
}
