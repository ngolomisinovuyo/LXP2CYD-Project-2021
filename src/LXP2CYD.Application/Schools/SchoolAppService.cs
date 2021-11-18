using Abp.Application.Services;
using Abp.Domain.Repositories;
using LXP2CYD.Schools.Dtos;
using LXP2CYD.LearnerModels.Schools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Bursaries
{
    public class SchoolAppService : AsyncCrudAppService<School, SchoolDto, int, PagedSchoolResultRequestDto, CreateSchoolDto, SchoolDto>, ISchoolAppService
    {
        public SchoolAppService(IRepository<School, int> repository): base(repository)
        {

        }
    }
}
