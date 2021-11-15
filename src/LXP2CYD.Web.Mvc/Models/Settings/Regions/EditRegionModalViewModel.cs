using System;
using System.Collections.Generic;
using LXP2CYD.Settings.Provinces.Dto;
using LXP2CYD.Settings.Regions.Dto;

namespace LXP2CYD.Web.Models.Settings.Regions
{
    public class EditRegionModalViewModel
    {
        public RegionDto Region { get; set; }

        public IReadOnlyList<ProvinceDto> Provinces { get; set; }
    }
}
