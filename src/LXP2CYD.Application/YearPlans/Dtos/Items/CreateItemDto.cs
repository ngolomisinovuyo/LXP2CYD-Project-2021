using System;
using Abp.AutoMapper;

namespace LXP2CYD.YearPlans.Dtos.Items
{
    [AutoMapTo(typeof(Item))]
    public class CreateItemDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int YearPlanId { get; set; }
    }
}
