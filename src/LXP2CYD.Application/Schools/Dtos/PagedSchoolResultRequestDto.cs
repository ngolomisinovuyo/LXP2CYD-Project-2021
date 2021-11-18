using Abp.Application.Services.Dto;
using System;

namespace LXP2CYD.Schools.Dtos
{
    //custom PagedResultRequestDto
    public class PagedSchoolResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
    }
}
