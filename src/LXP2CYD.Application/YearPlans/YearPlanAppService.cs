using Abp.Application.Services;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LXP2CYD.YearPlans.Dtos;

namespace LXP2CYD.YearPlans
{
    public class YearPlanAppService : AsyncCrudAppService<YearPlan, YearPlanDto, int, PagedYearPlanResultRequestDto, CreateYearPlanDto, YearPlanDto>, IYearPlanAppService
    {
        public YearPlanAppService(IRepository<YearPlan, int> repository): base(repository)
        {

        }
    }
}
