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
                        icon: "bi-speedometer2",
                        requiresAuthentication: true
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Tenants,
                        L("Centers"),
                        url: "Tenants",
                        icon: "bi-door-open",
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Tenants)
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Users,
                        L("Users"),
                        url: "Users",
                        icon: "bi-people",
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Users)
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Appointments,
                        L("Appointments"),
                        url: "Appointments",
                        icon: "bi-calendar",
                        requiresAuthentication: true,
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Appointments)
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.YearPlans,
                        L("YearPlans"),
                        url: "YearPlans",
                        icon: "bi-calendar3",
                        requiresAuthentication: true,
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Year_Plans)
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.YearPlans,
                        L("Bursaries"),
                        url: "Bursaries",
                        icon: "bi bi-mortarboard",
                        requiresAuthentication: true,
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Staff, PermissionNames.Pages_Learners)
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.YearPlans,
                        L("Programmes"),
                        url: "Programmes",
                        icon: "bi bi-calendar-event",
                        requiresAuthentication: true,
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Staff, PermissionNames.Pages_Learners)
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
                        icon: "bi-gear",
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Settings)
                    ).AddItem(
                            new MenuItemDefinition(
                                "Provinces",
                                new FixedLocalizableString("Provinces"),
                                url: "Provinces",
                                icon: "bi-building"
                            )
                        ).AddItem(
                            new MenuItemDefinition(
                                "Cities",
                                new FixedLocalizableString("Cities"),
                                url: "Cities",
                                icon: "bi-building"
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