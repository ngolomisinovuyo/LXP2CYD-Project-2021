using System;
using Abp.AutoMapper;

namespace LXP2CYD.Settings.Regions.Dto
{
    [AutoMapTo(typeof(Region))]
    public class CreateRegionDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProvinceId { get; set; }
    }
}
