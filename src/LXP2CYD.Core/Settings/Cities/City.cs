using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace LXP2CYD.Settings.Cities
{
    [Table("AppCities")]
    public class City: Entity<int>
    {
        public string Name { get; set; }
        public int ProvinceId { get; set; }
        
    }
}
