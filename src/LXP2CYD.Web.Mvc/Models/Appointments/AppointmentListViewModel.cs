using System;
using System.Collections.Generic;
using LXP2CYD.Users.Dto;

namespace LXP2CYD.Web.Models.Appointments
{
    public class AppointmentListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }
    }
}
