using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using LXP2CYD.LearnerModels.Subjects;
using LXP2CYD.Subjects.Dto;

namespace LXP2CYD.Leaners.Dtos.LearnerSubjects
{
    [AutoMapFrom(typeof(LearnerSubject))]
    [AutoMapTo(typeof(LearnerSubject))]
    public class LearnerSubjectDto: EntityDto<int>
    {
        public string GradeLevel { get; set; }
        public int TenantId { get; set; }
        public int SubjectId { get; set; }
        public long LearnerId { get; set; }
        public SubjectDto Subject { get; set; }
        public LearnerDto Learner { get; set; }
    }
}
