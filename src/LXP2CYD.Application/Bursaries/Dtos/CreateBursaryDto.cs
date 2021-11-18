using Abp.AutoMapper;
using LXP2CYD.LearnerModels.Bursaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Bursaries.Dtos
{
    [AutoMapTo(typeof(Bursary))]
    public class CreateBursaryDto
    {
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string Link { get; set; }
        public string DocumentUrl { get; set; }
    }
}
