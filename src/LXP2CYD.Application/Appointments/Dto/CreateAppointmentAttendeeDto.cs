using System;
using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;
using Abp.AutoMapper;

namespace LXP2CYD.Appointments.Dto
{
    [AutoMapTo(typeof(AppointmentAttendee))]
    public class CreateAppointmentAttendeeDto
    {
        public long AttendeeId { get; set; }
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }
    }
}
