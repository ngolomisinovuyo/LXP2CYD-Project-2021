﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using LXP2CYD.Authorization.Users;

namespace LXP2CYD.Appointments
{
    [Table("AppAppointmentAttendees")]
    public class AppintmentAttendee: FullAuditedEntity<int>, IMayHaveTenant
    {
        public int AppointmentId { get; set; }
        public long AttendeeId { get; set; }
        public DateTime Time { get; set; }
        public DateTime ArrivedAt { get; set; }
        public bool Arrived { get; set; }
        [ForeignKey(nameof(AppointmentId))]
        public Appointment Appointment { get; set; }
        [ForeignKey(nameof(AttendeeId))]
        public User Attendee { get; set; }
        public int? TenantId { get; set ; }
    }
}
