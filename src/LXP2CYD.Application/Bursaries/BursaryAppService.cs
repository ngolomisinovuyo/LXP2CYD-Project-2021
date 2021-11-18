using Abp.Application.Services;
using Abp.Domain.Repositories;
using LXP2CYD.Bursaries.Dtos;
using LXP2CYD.LearnerModels.Bursaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Bursaries
{
    public class BursaryAppService: AsyncCrudAppService<Bursary, BursaryDto, int, PagedBursaryResultRequestDto, CreateBursaryDto, BursaryDto>, IBursaryAppService
    {
        public BursaryAppService(IRepository<Bursary, int> repository): base(repository)
        {

        }
    }
}
