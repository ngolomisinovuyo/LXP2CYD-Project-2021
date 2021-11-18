using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using LXP2CYD.LearnerModels.Schools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Schools.Dtos
{
    [AutoMapFrom(typeof(School))]
    [AutoMapTo(typeof(School))]
    public class SchoolDto: EntityDto<int>
    {
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int? CityId { get; set; }
        public int? ProvinceId { get; set; }
    }
}
