using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using LXP2CYD.Authorization.Roles;
using LXP2CYD.Authorization.Users;
using LXP2CYD.MultiTenancy;

namespace LXP2CYD.EntityFrameworkCore
{
    public class LXP2CYDDbContext : AbpZeroDbContext<Tenant, Role, User, LXP2CYDDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public LXP2CYDDbContext(DbContextOptions<LXP2CYDDbContext> options)
            : base(options)
        {
        }
    }
}
