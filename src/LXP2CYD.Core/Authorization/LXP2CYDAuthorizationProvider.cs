using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace LXP2CYD.Authorization
{
    public class LXP2CYDAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Settings, L("Settings"));
            context.CreatePermission(PermissionNames.Pages_Staff, L("Employee"));
            context.CreatePermission(PermissionNames.Pages_Centers, L("Centers"));
            context.CreatePermission(PermissionNames.Pages_Appointments, L("Appointments"));
            context.CreatePermission(PermissionNames.Pages_Year_Plans, L("YearPlans"));
            context.CreatePermission(PermissionNames.Pages_Coordinators, L("Coordinators"));
            context.CreatePermission(PermissionNames.Pages_Liaisons, L("Liaisons"));
            context.CreatePermission(PermissionNames.Pages_Learners, L("Learners"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, LXP2CYDConsts.LocalizationSourceName);
        }
    }
}
