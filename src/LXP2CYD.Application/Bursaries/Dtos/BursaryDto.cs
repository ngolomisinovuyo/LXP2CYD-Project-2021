using Abp.Application.Services.Dto;
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
    [AutoMapFrom(typeof(Bursary))]
    public class BursaryDto: EntityDto<int>
    {
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string Link { get; set; }
        public string DocumentUrl { get; set; }
    }
}
