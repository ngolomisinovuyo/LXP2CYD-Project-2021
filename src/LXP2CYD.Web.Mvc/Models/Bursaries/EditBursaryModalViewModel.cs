using LXP2CYD.LearnerModels.Bursaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LXP2CYD.Web.Models.Bursaries
{
    public class EditBursaryModalViewModel
    {
        public Bursary  Bursary { get; set; }
        public IReadOnlyList<Bursary> Bursaries { get; set; }
    }
}
