using System.Collections.Generic;
using LXP2CYD.Roles.Dto;

namespace LXP2CYD.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}