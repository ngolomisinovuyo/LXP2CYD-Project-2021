using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Programmes.Dtos
{
    [AutoMapTo(typeof(Programme))]
    public class CreateProgrammeDto
    {
        public string Description { get; set; }
        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Duration { get; set; }

        public string Venue { get; set; }

        public string Link { get; set; }

        public string EventCategory { get; set; }

        public string ImageUrl { get; set; }
        public int? TenantId { get; set; }
    }
}
