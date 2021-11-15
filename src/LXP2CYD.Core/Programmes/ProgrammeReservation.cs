using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using LXP2CYD.Authorization.Users;

namespace LXP2CYD.Programmes
{
    [Table("AppProgrammeReservations")]
    public class ProgrammeReservation : FullAuditedEntity<int>, IMayHaveTenant
    {
        public int? TenantId { get; set; }
        public int ProgrammeId { get; set; }
        public Programme Programme { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }

        public string Feedback { get; set; }

        public bool attended { get; set; }

        public bool Enrolled { get; set; }
    }
}
