using System;
using Abp.Application.Services.Dto;

namespace LXP2CYD.Settings.Provinces.Dto
{
    public class PagedProvinceResultRequestDto: PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
    }
}
