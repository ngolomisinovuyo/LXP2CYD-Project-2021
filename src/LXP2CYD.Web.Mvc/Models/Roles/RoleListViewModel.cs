using System.Collections.Generic;
using LXP2CYD.Roles.Dto;

namespace LXP2CYD.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
