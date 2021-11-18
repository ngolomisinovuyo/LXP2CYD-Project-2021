using LXP2CYD.Programmes.Dtos;
using LXP2CYD.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LXP2CYD.Web.Models.Programmes
{
    public class EditProgrammeModalViewModel
    {
        public ProgrammeDto Programme { get; set; }
        public IReadOnlyList<UserDto> Users { get; set; }
    }
}
