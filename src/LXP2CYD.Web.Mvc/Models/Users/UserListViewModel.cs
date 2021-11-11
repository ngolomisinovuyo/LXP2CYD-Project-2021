using System.Collections.Generic;
using LXP2CYD.Roles.Dto;

namespace LXP2CYD.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
