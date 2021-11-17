
using LXP2CYD.Leaners.Dtos;
using LXP2CYD.Programmes.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LXP2CYD.Web.Models.Home
{
    public class HomePageViewModel
    {
        public IEnumerable<LearnerDto> Pearners { get; set; }
        public IEnumerable<ProgrammeDto> Programmes { get; set; }
        public int NoOfAccounts { get; set; }

    }
}
