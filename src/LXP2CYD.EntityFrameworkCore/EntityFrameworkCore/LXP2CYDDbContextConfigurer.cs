using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace LXP2CYD.EntityFrameworkCore
{
    public static class LXP2CYDDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<LXP2CYDDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<LXP2CYDDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
