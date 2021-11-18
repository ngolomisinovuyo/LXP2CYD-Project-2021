using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using LXP2CYD.LearnerModels.Subjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Subjects.Dto
{
    [AutoMapFrom(typeof(Subject))]
    [AutoMapTo(typeof(Subject))]
    public class SubjectDto: EntityDto<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
