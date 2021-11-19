using Abp.Application.Services;
using LXP2CYD.Leaners.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Leaners
{
    public interface ILearnerAppService: IAsyncCrudAppService<LearnerDto, long, PagedLearnerResultRequestDto, CreateLearnerDto, LearnerDto>
    {
    }
}
