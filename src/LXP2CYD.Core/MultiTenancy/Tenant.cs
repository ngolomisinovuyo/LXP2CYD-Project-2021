using System.ComponentModel.DataAnnotations.Schema;
using Abp.MultiTenancy;
using LXP2CYD.Authorization.Users;
using LXP2CYD.Settings.Regions;

namespace LXP2CYD.MultiTenancy
{
    /// <summary>
    /// This is a Center entity
    /// </summary>
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
        public string PhoneNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string LogoUrl { get; set; }
        public string WebsiteUrl { get; set; }
        public CenterType Type { get; set; }
        public int? RegionId { get; set; }
        [ForeignKey(nameof(RegionId))]
        public Region Region { get; set; }
    }
}
