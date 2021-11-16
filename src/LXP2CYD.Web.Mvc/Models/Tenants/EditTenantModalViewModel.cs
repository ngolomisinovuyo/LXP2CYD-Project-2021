using System;
using System.Collections.Generic;
using LXP2CYD.MultiTenancy.Dto;
using LXP2CYD.Settings.Regions.Dto;

namespace LXP2CYD.Web.Models.Tenants
{
    public class EditTenantModalViewModel
    {
        public TenantDto Tenant { get; set; }

        public IReadOnlyList<RegionDto> Regions { get; set; }
    }
}
