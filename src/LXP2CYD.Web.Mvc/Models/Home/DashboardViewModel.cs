using LXP2CYD.Leaners.Dtos;
using LXP2CYD.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LXP2CYD.Web.Models.Home
{
    public class DashboardViewModel
    {
        public int RegionsCount { get; set; }
        public int CenterCount { get; set; }
        public int LearnersCount { get; set; }
        public int StaffCount { get; set; }

        public IReadOnlyList<UserDto> Users { get; set; }
    }
}
