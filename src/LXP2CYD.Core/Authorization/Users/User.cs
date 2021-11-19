using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Authorization.Users;
using Abp.Extensions;
using LXP2CYD.Authorization.Users.Staffs;
using LXP2CYD.LearnerModels.Learners;
using LXP2CYD.Settings.Provinces;
using LXP2CYD.Settings.Regions;

namespace LXP2CYD.Authorization.Users
{
    public class User : AbpUser<User>
    {
        public const string DefaultPassword = "123qwe";

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public Gender Gender { get; set; }
        public string PostalCode { get; set; }
        public int? ProvinceId { get; set; }
        public int? RegionId { get; set; }
        [ForeignKey(nameof(ProvinceId))]
        public Province Province { get; set; }

        [ForeignKey(nameof(RegionId))]
        public Region Region { get; set; }

        public Staff Staff { get; set; }
        public Learner Learner { get; set; }

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress, string name=AdminUserName, string surname=AdminUserName)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = emailAddress,
                Name = name,
                Surname = surname,
                EmailAddress = emailAddress,
                Roles = new List<UserRole>()
            };

            user.SetNormalizedNames();

            return user;
        }
    }
}
