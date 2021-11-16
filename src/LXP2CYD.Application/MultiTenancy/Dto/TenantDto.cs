using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.MultiTenancy;
using LXP2CYD.Settings.Regions.Dto;
using LXP2CYD.Users.Dto;

namespace LXP2CYD.MultiTenancy.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantDto : EntityDto
    {
        [Required]
        [StringLength(AbpTenantBase.MaxTenancyNameLength)]
        [RegularExpression(AbpTenantBase.TenancyNameRegex)]
        public string TenancyName { get; set; }

        [Required]
        [StringLength(AbpTenantBase.MaxNameLength)]
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string LogoUrl { get; set; }
        public string WebsiteUrl { get; set; }
        public CenterType Type { get; set; }
        public int? RegionId { get; set; }
        public RegionDto Region { get; set; }
        public UserDto Manager { get; set; }

        public bool IsActive {get; set;}
    }
}
