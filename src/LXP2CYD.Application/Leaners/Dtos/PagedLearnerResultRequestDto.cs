using System;
using Abp.Application.Services.Dto;

namespace LXP2CYD.Leaners.Dtos
{
    public class PagedLearnerResultRequestDto: PagedResultRequestDto
    {
        public bool? IsActive { get; set; }
    }
}
