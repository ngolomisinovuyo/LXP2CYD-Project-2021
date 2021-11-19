using Abp.Application.Services;
using LXP2CYD.YearPlans.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.YearPlans
{
    public interface IYearPlanAppService: IAsyncCrudAppService<YearPlanDto, int, PagedYearPlanResultRequestDto, CreateYearPlanDto, YearPlanDto>
    {
    }
}
