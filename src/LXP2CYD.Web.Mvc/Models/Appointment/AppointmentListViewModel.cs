using System;
using System.Collections.Generic;
using LXP2CYD.Users.Dto;

namespace LXP2CYD.Web.Models.Appointment
{
    public class AppointmentListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }
    }
}
