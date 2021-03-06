using System.Collections.Generic;
using LXP2CYD.Roles.Dto;
using LXP2CYD.Settings.Provinces.Dto;
using LXP2CYD.Settings.Regions.Dto;

namespace LXP2CYD.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
        public IReadOnlyList<ProvinceDto> Provinces { get; set; }
        public IReadOnlyList<RegionDto> Regions { get; set; }
    }
}
