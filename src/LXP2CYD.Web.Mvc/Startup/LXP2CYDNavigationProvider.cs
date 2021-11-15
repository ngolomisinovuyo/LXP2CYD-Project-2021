using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Localization;
using LXP2CYD.Authorization;

namespace LXP2CYD.Web.Startup
{
    /// <summary>
    /// This class defines menus for the application.
    /// </summary>
    public class LXP2CYDNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Dashboard,
                        L("Dashboard"),
                        url: "",
                        icon: "fas fa-tachometer-alt",
                        requiresAuthentication: true
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Tenants,
                        L("Centers"),
                        url: "Tenants",
                        icon: "fas fa-building",
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Tenants)
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Users,
                        L("Users"),
                        url: "Users",
                        icon: "fas fa-users",
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Users)
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Appointments,
                        L("Appointments"),
                        url: "Appointments",
                        icon: "fas fa-calendar",
                        requiresAuthentication: true,
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Appointments)
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Roles,
                        L("Roles"),
                        url: "Roles",
                        icon: "fas fa-theater-masks",
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Roles)
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Centers,
                        L("Regions"),
                        url: "Regions",
                        icon: "fas fa-building",
                        requiresAuthentication: true,
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Centers)
                    )
                )
                .AddItem( // Menu items below is just for demonstration!
                    new MenuItemDefinition(
                        "Settings",
                        L("Settings"),
                        icon: "fas fa-cog"
                    ).AddItem(
                            new MenuItemDefinition(
                                "Provinces",
                                new FixedLocalizableString("Provinces"),
                                url: "Provinces",
                                icon: "fas fa-building"
                            )
                        ).AddItem(
                            new MenuItemDefinition(
                                "Cities",
                                new FixedLocalizableString("Cities"),
                                url: "Cities",
                                icon: "fas fa-city"
                            )
                    )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, LXP2CYDConsts.LocalizationSourceName);
        }
    }
}