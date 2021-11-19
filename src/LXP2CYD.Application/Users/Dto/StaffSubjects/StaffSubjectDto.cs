using System;
using Abp.Application.Services.Dto;
using LXP2CYD.Subjects.Dto;

namespace LXP2CYD.Users.Dto.StaffSubjects
{
    public class StaffSubjectDto: EntityDto<int>
    {
        public double? Rate { get; set; }
        public int TenantId { get; set; }
        public int SubjectId { get; set; }
        public long StaffId { get; set; }
        public SubjectDto Subject { get; set; }
        public StaffDto Staff { get; set; }
    }
}
