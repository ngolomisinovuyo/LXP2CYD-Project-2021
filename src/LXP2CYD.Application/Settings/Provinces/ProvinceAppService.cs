using System;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using LXP2CYD.Settings.Provinces.Dto;

namespace LXP2CYD.Settings.Provinces
{
    public class ProvinceAppService: AsyncCrudAppService<Province, ProvinceDto, int, PagedProvinceResultRequestDto, CreateProvinceDto, ProvinceDto>, IProvinceAppService
    {
        private readonly IRepository<Province, int> _provinceRepository;
        public ProvinceAppService(IRepository<Province, int> provinceRepository) :base(provinceRepository)
        {
        }
    }
}
