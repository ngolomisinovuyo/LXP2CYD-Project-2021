using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using LXP2CYD.MultiTenancy.Dto;
using LXP2CYD.Settings.Regions.Dto;

namespace LXP2CYD.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
        Task<IReadOnlyList<RegionDto>> GetRegions();
    }
}

