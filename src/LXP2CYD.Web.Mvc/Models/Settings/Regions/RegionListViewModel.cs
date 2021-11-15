using System;
using System.Collections.Generic;
using LXP2CYD.Settings.Provinces.Dto;

namespace LXP2CYD.Web.Models.Settings.Regions
{
    public class RegionListViewModel
    {
        public IReadOnlyList<ProvinceDto> Provinces { get; set; }
    }
}
