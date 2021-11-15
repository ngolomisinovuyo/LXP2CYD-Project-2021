using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace LXP2CYD.LearnerModels.Bursaries
{
    [Table("AppBursaries")]
    public class Bursary: Entity<int>
    {
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string Link { get; set; }
        public string DocumentUrl { get; set; }
    }
}
