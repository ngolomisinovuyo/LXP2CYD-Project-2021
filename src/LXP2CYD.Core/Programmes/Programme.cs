using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace LXP2CYD.Programmes
{
    [Table("AppProgrammes")]
    public class Programme: FullAuditedEntity<int>, IMayHaveTenant
    {
        public string Description { get; set; }
        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Duration { get; set; }

        public string Venue { get; set; }

        public string Link { get; set; }

        public ProgrammeCategory Category { get; set; }

        public string ImageUrl { get; set; }
        public int? TenantId { get; set; }
        public virtual IList<ProgrammeReservation> Rsvps { get; set; }
    }
}
