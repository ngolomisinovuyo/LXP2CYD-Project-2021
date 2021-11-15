using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace LXP2CYD.LearnerModels.Schools
{
    [Table("AppSchools")]
    public class School: Entity<int>
    {
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int? CityId { get; set; }
        public int? ProvinceId { get; set; }
    }
}
