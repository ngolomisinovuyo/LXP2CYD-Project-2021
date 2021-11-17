using Abp.Application.Services;
using Abp.Domain.Repositories;
using LXP2CYD.Programmes.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Programmes
{
    public class ProgrammeAppService: AsyncCrudAppService<Programme, ProgrammeDto, int, PagedProgrammeResultRequestDto, CreateProgrammeDto, ProgrammeDto>, IProgrammeAppService
    {
        public ProgrammeAppService(IRepository<Programme, int> repository): base(repository)
        {

        }
    }
}
