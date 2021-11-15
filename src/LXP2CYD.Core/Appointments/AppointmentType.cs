using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace LXP2CYD.Appointments
{
    [Table("AppAppointmentTypes")]
    public class AppointmentType: Entity<int>
    {
        public string Name { get; set; }
    }
}
