using System;
using System.Collections.Generic;
using LXP2CYD.Settings.Regions.Dto;

namespace LXP2CYD.Web.Models.Tenants
{
    public class TenantListViewModel
    {
        public IReadOnlyList<RegionDto> Regions { get; set; }
    }
}
