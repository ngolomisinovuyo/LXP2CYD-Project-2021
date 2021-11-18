using Abp.AutoMapper;
using LXP2CYD.LearnerModels.Subjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Subjects.Dto
{
    [AutoMapTo(typeof(Subject))]
    public class CreateSubjectDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
