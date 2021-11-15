using System;
using Abp.Application.Services.Dto;

namespace LXP2CYD.Settings.Regions.Dto
{
    public class PagedRegionResultRequestDto: PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
    }
}
