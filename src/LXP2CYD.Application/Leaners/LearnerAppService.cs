using Abp.Application.Services;
using Abp.Domain.Repositories;
using LXP2CYD.Leaners.Dtos;
using LXP2CYD.LearnerModels.Learners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Leaners
{
    public class LearnerAppService : AsyncCrudAppService<Learner, LearnerDto, long, PagedLearnerResultRequestDto, CreateLearnerDto, LearnerDto>, ILearnerAppService
    {
        public LearnerAppService(IRepository<Learner, long> repository): base(repository)
        {

        }
    }
}
