using Abp.Application.Services.Dto;
using System;

namespace LXP2CYD.Subjects.Dto
{
    //custom PagedResultRequestDto
    public class PagedSubjectResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
    }
}
