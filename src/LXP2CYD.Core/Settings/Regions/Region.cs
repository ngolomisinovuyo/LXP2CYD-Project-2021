using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using LXP2CYD.MultiTenancy;
using LXP2CYD.Settings.Provinces;

namespace LXP2CYD.Settings.Regions
{
    [Table("AppRegions")]
    public class Region: AuditedEntity<int>
    {
        public Region()
        {
            Centers = new List<Tenant>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProvinceId { get; set; }
        [ForeignKey(nameof(ProvinceId))]
        public Province Province { get; set; }
        public IList<Tenant> Centers { get; set; }
    }
}
