using System;
using System.Collections.Generic;
using LXP2CYD.Appointments.Dto;
using LXP2CYD.Users.Dto;

namespace LXP2CYD.Web.Models.Appointment
{
    public class EditAppointmentModalViewModel
    {
        public AppointmentDto Appointment { get; set; }
        public IReadOnlyList<UserDto> Users { get; set; }
    }
}
