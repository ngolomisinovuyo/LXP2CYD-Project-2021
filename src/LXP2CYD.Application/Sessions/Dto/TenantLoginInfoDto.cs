using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using LXP2CYD.MultiTenancy;

namespace LXP2CYD.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}
