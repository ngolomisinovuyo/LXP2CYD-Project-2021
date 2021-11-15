using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace LXP2CYD.LearnerModels.Resources
{
    [Table("AppResources")]
    public class Resource : FullAuditedEntity<int>, IMustHaveTenant
    {
        public string Title { get; set; }
        public int TenantId { get; set; }
        public int? SubjectId { get; set; }
        public long? UserId { get; set; }
        public string Description { get; set; }
        public ResourceType Type { get; set; }
        public string DocumentUrl { get; set; }
    }
}
