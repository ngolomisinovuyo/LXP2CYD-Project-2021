using System;
using Abp.AutoMapper;

namespace LXP2CYD.Settings.Provinces.Dto
{
    [AutoMapTo(typeof(Province))]
    public class CreateProvinceDto
    {
        public string Name { get; set; }
    }
}
