using Abp.Application.Services;
using LXP2CYD.Programmes.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Programmes
{
    public interface IProgrammeAppService: IAsyncCrudAppService<ProgrammeDto, int, PagedProgrammeResultRequestDto, CreateProgrammeDto, ProgrammeDto>
    {
    }
}
