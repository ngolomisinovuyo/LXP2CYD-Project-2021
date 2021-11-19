using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using LXP2CYD.Authorization.Users.Staffs;
using LXP2CYD.Users.Dto.StaffSubjects;

namespace LXP2CYD.Users.Dto
{
    [AutoMapFrom(typeof(Staff))]
    [AutoMapTo(typeof(Staff))]
    public class StaffDto: EntityDto<long>
    {
        public int TenantId { get; set; }
        public long UserId { get; set; }

        public StaffType Type { get; set; }
        public string Duties { get; set; }
        public IReadOnlyList<StaffSubjectDto> StaffSubjects { get; set; }
    }
}
