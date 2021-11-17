using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using LXP2CYD.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Appointments.Dto
{
    [AutoMapFrom(typeof(AppointmentAttendee))]
    [AutoMapTo(typeof(AppointmentAttendee))]
    public class AppointmentAttendeeDto: EntityDto<int>
    {
        public int AppointmentId { get; set; }
        public long AttendeeId { get; set; }
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }
        public DateTime Time { get; set; }
        public DateTime ArrivedAt { get; set; }
        public bool Arrived { get; set; }
        public AppointmentDto Appointment { get; set; }
        public UserDto Attendee { get; set; }
        public int? TenantId { get; set; }
    }
}
