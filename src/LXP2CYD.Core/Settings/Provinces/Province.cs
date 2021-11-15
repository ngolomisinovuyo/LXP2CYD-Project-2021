using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using LXP2CYD.Settings.Cities;

namespace LXP2CYD.Settings.Provinces
{
    [Table("AppProvinces")]
    public class Province: Entity<int>
    {
        public string Name { get; set; }
        public IList<City> Cities { get; set; }
    }
}
