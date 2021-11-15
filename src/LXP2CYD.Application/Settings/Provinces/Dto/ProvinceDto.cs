using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace LXP2CYD.Settings.Provinces.Dto
{
    [AutoMapFrom(typeof(Province))]
    public class ProvinceDto: EntityDto<int>
    {
        public string Name { get; set; }
    }
}
