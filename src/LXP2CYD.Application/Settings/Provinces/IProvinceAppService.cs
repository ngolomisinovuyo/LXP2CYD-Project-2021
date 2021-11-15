using System;
using Abp.Application.Services;
using LXP2CYD.Settings.Provinces.Dto;

namespace LXP2CYD.Settings.Provinces
{
    public interface IProvinceAppService: IAsyncCrudAppService<ProvinceDto, int, PagedProvinceResultRequestDto, CreateProvinceDto, ProvinceDto>
    {
        
    }
}
