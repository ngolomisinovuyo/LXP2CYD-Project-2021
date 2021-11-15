using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using LXP2CYD.Settings.Provinces;
using LXP2CYD.Settings.Provinces.Dto;
using LXP2CYD.Settings.Regions.Dto;
using Microsoft.EntityFrameworkCore;

namespace LXP2CYD.Settings.Regions
{
    public class RegionAppService : AsyncCrudAppService<Region, RegionDto, int, PagedRegionResultRequestDto, CreateRegionDto, RegionDto>, IRegionAppService
    {
        private readonly IRepository<Region, int> _regionRepository;
        private readonly IRepository<Province, int> _provinceRepository;
        public RegionAppService(IRepository<Region, int> regionRepository,
            IRepository<Province, int> provinceRepository) : base(regionRepository)
        {
            _regionRepository = regionRepository;
            _provinceRepository = provinceRepository;
        }
        public async override Task<PagedResultDto<RegionDto>> GetAllAsync(PagedRegionResultRequestDto input)
        {
            var regionDtos = await _regionRepository.GetAll()
                .Include(x=> x.Province)
                .Include(x => x.Centers)
                .WhereIf(!input.Keyword.IsNullOrEmpty(), x=>x.Name.Contains(input.Keyword))
                .Select(x => new RegionDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    ProvinceId = x.ProvinceId,
                    Province = new ProvinceDto { Id = x.Province.Id, Name = x.Province.Name},
                    NoOfCenters = x.Centers.Count
                })
                .Skip(input.SkipCount).Take(input.MaxResultCount).OrderBy(x=>x.Name)
                .ToListAsync();
            return new PagedResultDto<RegionDto>(0, regionDtos);
        }

        public async Task<IReadOnlyList<ProvinceDto>> GetProvinces()
        {
            var provinces = await _provinceRepository.GetAllListAsync();
            return ObjectMapper.Map<IReadOnlyList<ProvinceDto>>(provinces);
        }

    }
}
