using Abp.Application.Services;
using LXP2CYD.MultiTenancy.Dto;

namespace LXP2CYD.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

