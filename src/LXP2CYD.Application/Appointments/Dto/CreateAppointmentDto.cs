using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;

namespace LXP2CYD.Appointments.Dto
{
    [AutoMapTo(typeof(Appointment))]
    public class CreateAppointmentDto
    {
        [Required]
        public string Title { get; set; }
        public long HostId { get; set; }
        public AppointmentType Type { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Notes { get; set; }
        public bool IsVirtual { get; set; }
        public string MeetingLink { get; set; }
        public string Location { get; set; }
        public int? TenantId { get; set; }
        public IReadOnlyList<CreateAppointmentAttendeeDto> CreateAppointmentAttendeeDtos { get; set; }
    }
}
