using Abp.AutoMapper;
using LXP2CYD.LearnerModels.Learners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Leaners.Dtos
{
    [AutoMapTo(typeof(Learner))]
    public class CreateLearnerDto
    {
        public long UserId { get; set; }
        public string IdNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Stream { get; set; }
        public int? SchoolId { get; set; }
        public Grade? Grade { get; set; }
    }
}
