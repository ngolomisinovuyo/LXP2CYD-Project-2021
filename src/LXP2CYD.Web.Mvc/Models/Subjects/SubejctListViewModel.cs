using LXP2CYD.Subjects.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LXP2CYD.Web.Models.Subjects
{
    public class SubejctListViewModel
    {
        public IReadOnlyList<SubjectDto> Subjects { get; set; }
    }
}
