using System.Collections.Generic;
using System.Linq;
using LXP2CYD.Roles.Dto;
using LXP2CYD.Settings.Provinces.Dto;
using LXP2CYD.Settings.Regions.Dto;
using LXP2CYD.Users.Dto;

namespace LXP2CYD.Web.Models.Users
{
    public class EditUserModalViewModel
    {
        public UserDto User { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
        public IReadOnlyList<ProvinceDto> Provinces { get; set; }
        public IReadOnlyList<RegionDto> Regions { get; set; }

        public bool UserIsInRole(RoleDto role)
        {
            return User.RoleNames != null && User.RoleNames.Any(r => r == role.NormalizedName);
        }
    }
}
