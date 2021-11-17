using LXP2CYD.Leaners.Dtos;
using LXP2CYD.MultiTenancy.Dto;
using LXP2CYD.Programmes.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LXP2CYD.Web.Models.Admin
{
    public class CertificateViewModel
    {
        public LearnerDto Learner { get; set; }
        public TenantDto CenterDetails { get; set; }
        public ProgrammeDto Programme { get; set; }
    }
}
