using LXP2CYD.Bursaries.Dtos;
using LXP2CYD.LearnerModels.Bursaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LXP2CYD.Web.Models.Bursaries
{
    public class EditBursaryModalViewModel
    {
        public BursaryDto  Bursary { get; set; }
        public IReadOnlyList<BursaryDto> Bursaries { get; set; }
    }
}
