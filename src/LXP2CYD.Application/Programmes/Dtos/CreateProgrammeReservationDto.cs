using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Programmes.Dtos
{
    [AutoMapTo(typeof(ProgrammeReservation))]
    public class CreateProgrammeReservationDto
    {
        public int? TenantId { get; set; }
        public int ProgrammeId { get; set; }

        public long UserId { get; set; }
        public string Feedback { get; set; }

        public bool attended { get; set; }

        public bool Enrolled { get; set; }
    }
}
