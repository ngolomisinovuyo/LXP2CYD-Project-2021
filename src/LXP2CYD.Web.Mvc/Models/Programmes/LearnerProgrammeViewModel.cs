using LXP2CYD.Programmes.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LXP2CYD.Web.Models.Programmes
{
    public class LearnerProgrammeViewModel
    {
        public ProgrammeReservationDto ProgrammeReservation { get; set; }
        public IReadOnlyList<ProgrammeReservationDto> ProgrammeReservations { get; set; }
        public IReadOnlyList<ProgrammeDto> Programmes { get; set; }
        public ProgrammeDto Programme { get; set; }
    }
}
