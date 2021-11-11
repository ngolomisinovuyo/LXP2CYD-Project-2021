using Abp.Authorization;
using LXP2CYD.Authorization.Roles;
using LXP2CYD.Authorization.Users;

namespace LXP2CYD.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
