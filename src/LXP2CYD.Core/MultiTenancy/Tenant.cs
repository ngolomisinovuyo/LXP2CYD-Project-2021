using Abp.MultiTenancy;
using LXP2CYD.Authorization.Users;

namespace LXP2CYD.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
