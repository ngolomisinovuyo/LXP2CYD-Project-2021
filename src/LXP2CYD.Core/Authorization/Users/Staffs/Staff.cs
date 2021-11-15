using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace LXP2CYD.Authorization.Users.Staffs
{
    [Table("AppStaffs")]
    public class Staff : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long UserId { get; set; }
        public string Duties { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        
    }
}
