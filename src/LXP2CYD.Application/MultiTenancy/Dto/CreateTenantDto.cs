using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.MultiTenancy;

namespace LXP2CYD.MultiTenancy.Dto
{
    [AutoMapTo(typeof(Tenant))]
    public class CreateTenantDto
    {
        [Required]
        [StringLength(AbpTenantBase.MaxTenancyNameLength)]
        [RegularExpression(AbpTenantBase.TenancyNameRegex)]
        public string TenancyName { get; set; }

        [Required]
        [StringLength(AbpTenantBase.MaxNameLength)]
        public string Name { get; set; }

        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxPhoneNumberLength)]
        public string PhoneNumber { get; set; }


        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        public int? RegionId { get; set; }

        public long? MangerId { get; set; }

        // Manager Information
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string ManagerEmailAddress { get; set; }

        [StringLength(AbpUserBase.MaxPhoneNumberLength)]
        public string ManagerPhoneNumber { get; set; }

        [StringLength(AbpUserBase.MaxNameLength)]
        public string ManagerName { get; set; }
        [StringLength(AbpUserBase.MaxSurnameLength)]
        public string ManagerSurname { get; set; }
        public bool IsActive {get; set;}
    }
}
