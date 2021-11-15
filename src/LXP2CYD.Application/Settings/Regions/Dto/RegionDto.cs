using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using LXP2CYD.MultiTenancy.Dto;
using LXP2CYD.Settings.Provinces.Dto;

namespace LXP2CYD.Settings.Regions.Dto
{
    [AutoMapFrom(typeof(Region))]
    public class RegionDto: EntityDto<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProvinceId { get; set; }
        public int NoOfCenters { get; set; }
        public ProvinceDto Province { get; set; }
        public IReadOnlyList<TenantDto> Centers { get; set; }
    }
}
