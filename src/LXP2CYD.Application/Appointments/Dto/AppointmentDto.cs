using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using LXP2CYD.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Appointments.Dto
{
    [AutoMapFrom(typeof(Appointment))]
    [AutoMapTo(typeof(Appointment))]
    public class AppointmentDto: EntityDto<int>
    {
        public long HostId { get; set; }
        public AppointmentType Type { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Notes { get; set; }
        public bool IsVirtual { get; set; }
        public string MeetingLink { get; set; }
        public string Location { get; set; }
        public UserDto Host { get; set; }
        public int? TenantId { get; set; }
    }
}
