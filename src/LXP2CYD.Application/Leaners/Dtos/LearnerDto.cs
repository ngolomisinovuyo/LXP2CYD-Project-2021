using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using LXP2CYD.Leaners.Dtos.LearnerSubjects;
using LXP2CYD.LearnerModels.Learners;
using LXP2CYD.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Leaners.Dtos
{
    [AutoMapFrom(typeof(Learner))]
    [AutoMapTo(typeof(Learner))]
    public class LearnerDto: EntityDto<long>
    {
        public int TenantId { get; set; }
        public long UserId { get; set; }
        public string IdNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Stream { get; set; }
        public int? SchoolId { get; set; }
        public Grade? Grade { get; set; }

        public UserDto User { get; set; }
        public IReadOnlyList<LearnerSubjectDto> LearnerSubjects { get; set; }
    }
}
