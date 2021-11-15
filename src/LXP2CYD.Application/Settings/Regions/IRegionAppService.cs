using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using LXP2CYD.Settings.Provinces.Dto;
using LXP2CYD.Settings.Regions.Dto;

namespace LXP2CYD.Settings.Regions
{
    public interface IRegionAppService: IAsyncCrudAppService<RegionDto, int, PagedRegionResultRequestDto, CreateRegionDto, RegionDto>
    {
        Task<IReadOnlyList<ProvinceDto>> GetProvinces();
    }
}
