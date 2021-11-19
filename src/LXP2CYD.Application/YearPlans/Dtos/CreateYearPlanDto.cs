using System;
namespace LXP2CYD.YearPlans.Dtos
{
    public class CreateYearPlanDto
    {
        public int Year { get; set; }
        public int? TenantId { get; set; }
        public Status Status { get; set; }
        public string Descrioption { get; set; }
    }
}
