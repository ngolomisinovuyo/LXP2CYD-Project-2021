using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using LXP2CYD.Authorization.Users;

namespace LXP2CYD.Appointments
{
    [Table("AppAppointments")]
    public class Appointment: FullAuditedEntity<int>, IMayHaveTenant
    {
        public long HostId { get; set; }
        public AppointmentType Type { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Notes { get; set; }
        public bool IsVirtual { get; set; }
        public string MeetingLink { get; set; }
        public string Location { get; set; }
        [ForeignKey(nameof(HostId))]
        public User Host { get; set; }
        public int? TenantId { get; set; }
    }
}
