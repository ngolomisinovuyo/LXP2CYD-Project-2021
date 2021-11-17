using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using LXP2CYD.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Programmes.Dtos
{
    [AutoMapTo(typeof(ProgrammeReservation))]
    [AutoMapFrom(typeof(ProgrammeReservation))]
    public class ProgrammeReservationDto: EntityDto<int>
    {
        public int? TenantId { get; set; }
        public int ProgrammeId { get; set; }
        public ProgrammeDto Programme { get; set; }

        public long UserId { get; set; }
        public UserDto User { get; set; }

        public string Feedback { get; set; }

        public bool attended { get; set; }

        public bool Enrolled { get; set; }
    }
}
