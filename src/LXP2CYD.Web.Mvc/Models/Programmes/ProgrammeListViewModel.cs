using LXP2CYD.Programmes.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LXP2CYD.Web.Models.Programmes
{
    public class ProgrammeListViewModel
    {
        public IReadOnlyList<ProgrammeDto> Programmes { get; set; }
    }
}
